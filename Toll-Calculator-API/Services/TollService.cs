using System;
using System.Threading.Tasks;
using Toll_Calculator_API.DbModels;
using Toll_Calculator_API.Models;

namespace Toll_Calculator_API.Services
{
    public interface ITollService
    {
        Task<ServiceResult<VehicleTollEvent>> AddTollEvent(VehicleTollEvent vehicleTollEvent);
    }

    public class TollService : ITollService
    {
        private tollContext _context;

        public TollService(tollContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<VehicleTollEvent>> AddTollEvent(VehicleTollEvent vehicleTollEvent)
        {
            try
            {
                await _context.VehicleTollEvents.AddAsync(vehicleTollEvent);
                await _context.SaveChangesAsync();

                return new ServiceResult<VehicleTollEvent>(vehicleTollEvent);
            }
            catch (Exception ex)
            {
                return new ServiceResult<VehicleTollEvent>(ex);
            }
        }
    }
}
