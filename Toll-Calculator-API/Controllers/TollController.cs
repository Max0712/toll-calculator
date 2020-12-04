using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toll_Calculator_API.Models;
using System.ComponentModel.DataAnnotations;
using Toll_Calculator_API.DbModels;
using Toll_Calculator_API.Services;
using AutoMapper;

namespace Toll_Calculator_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TollController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITollService _tollService;

        public TollController(IMapper mapper, ITollService tollService)
        {
            _mapper = mapper;
            _tollService = tollService;
        }

        [Route("event")]
        [HttpPost]
        public async Task<IActionResult> RegisterTollEvent([FromBody]TollEventRegistration body)
        {
            var insertResult = await _tollService.AddTollEvent(new VehicleTollEvent
            {
                EventTime = body.EventTime.Value,
                RegistrationNumber = body.RegistrationNumber                
            });

            if (!insertResult.IsSuccessStatusCode())
                return insertResult.ToHttpResult();

            // Här hade man kunnat låta processen fortsätta på annan tråd i bakgrunden.
            await VehicleToTollUpdate(body, insertResult.Result.Id);

            return Ok(body);
        }

        private async Task<Exception> VehicleToTollUpdate(TollEventRegistration body, long tollEventId)
        {
            var vehicle = await _tollService.SelectVehicle(body.RegistrationNumber);
            if (vehicle == null)
            {
                var vehicleType = await _tollService.SelectVehicleType(body.VehicleType);
                var vehicleResult = await _tollService.AddVehicle(new Vehicle
                {
                    VehicleType = vehicleType,
                    RegistrationNumber = body.RegistrationNumber
                });

                //Här vill man självklart ha en riktig lösning
                if (!vehicleResult.IsSuccessStatusCode())
                    return vehicleResult.Exception;

                vehicle = vehicleResult.Result;
            }

            var updateResult = await _tollService.SetTollEventVehicle(tollEventId, vehicle.Id);
            if (!updateResult.IsSuccessStatusCode())
                return updateResult.Exception;

            return null;
        }
    }
}
