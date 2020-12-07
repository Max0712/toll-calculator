using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toll_Calculator_API.Models;
using Toll_Calculator_API.DbModels;
using Toll_Calculator_API.Services;
using AutoMapper;
using Toll_Calculator_API.Calculations;

namespace Toll_Calculator_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TollController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITollService _tollService;
        private readonly ITollCalulations _tollCalculations;

        public TollController(IMapper mapper, ITollService tollService, ITollCalulations tollCalulations)
        {
            _mapper = mapper;
            _tollService = tollService;
            _tollCalculations = tollCalulations;
        }

        [Route("event")]
        [HttpPost]
        public async Task<IActionResult> RegisterTollEvent([FromBody] TollEventRegistration body)
        {
            var insertResult = await _tollService.AddTollEvent(new VehicleTollEvent
            {
                EventTime = body.EventTime.Value,
                RegistrationNumber = body.RegistrationNumber
            });

            if (!insertResult.IsSuccessStatusCode())
                return insertResult.ToHttpResult();

            // Här hade man kunnat låta processen fortsätta på annan tråd i bakgrunden.
            var updateResult = await VehicleToTollUpdate(body, insertResult.Result.Id);
            if (!updateResult.IsSuccessStatusCode())
                return updateResult.ToHttpResult();

            return Ok(body);
        }

        [Route("sum")]
        [HttpGet]
        public async Task<IActionResult> GetVehicleTollSum(string registrationNumber, DateTime fromDate, DateTime toDate)
        {
            var vehicle = await _tollService.SelectVehicleAndEvents(registrationNumber, fromDate, toDate);
            if (vehicle.VehicleType.IsFree)
                return Ok(new VehicleTollSummary());

            var result = await _tollCalculations.CalculateTollSummary(vehicle.VehicleTollEvents, fromDate, toDate);

            return Ok(result);
        }

        private async Task<ServiceResult<bool>> VehicleToTollUpdate(TollEventRegistration body, long tollEventId)
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
                    return new ServiceResult<bool>(vehicleResult.Exception);

                vehicle = vehicleResult.Result;
            }

            var updateResult = await _tollService.SetTollEventVehicle(tollEventId, vehicle.Id);
            if (!updateResult.IsSuccessStatusCode())
                return new ServiceResult<bool>(updateResult.Exception);

            return new ServiceResult<bool>(true);
        }
    }
}
