using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toll_Calculator_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Toll_Calculator_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TollController : ControllerBase
    {
        [Route("event")]
        [HttpPost]
        public async Task<IActionResult> RegisterTollEvent([FromBody]TollEventRegistration body)
        {
            //if (!ModelState.IsValid)
            //    return UnprocessableEntity(body);

            return BadRequest(new NotImplementedException());
            // Automappa detta till rätt modeller

            // Insert till VehicleTollEvent
            // (Eventuellt yield return med OK)

            // Kolla om regnr finns i vehicle lägg till eller uppdatera vehicle-type

        }
    }
}
