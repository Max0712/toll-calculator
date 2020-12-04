using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toll_Calculator_API.DbModels;
using Toll_Calculator_API.Models;

namespace Toll_Calculator_API.Services
{
    public interface ITollService
    {
        Task<ServiceResult<VehicleTollEvent>> AddTollEvent(VehicleTollEventModel vehicleTollEvent);
    }

    public class TollService : ITollService
    {
        public async Task<ServiceResult<VehicleTollEvent>> AddTollEvent(VehicleTollEventModel vehicleTollEvent)
        {
            return null;
        }
    }
}
