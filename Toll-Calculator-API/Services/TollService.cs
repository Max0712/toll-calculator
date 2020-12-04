using System;
using System.Threading.Tasks;
using Toll_Calculator_API.DbModels;
using Toll_Calculator_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Toll_Calculator_API.Services
{
    public interface ITollService
    {
        Task<ServiceResult<VehicleTollEvent>> AddTollEvent(VehicleTollEvent vehicleTollEvent);
        Task<Vehicle> SelectVehicle(string registrationNumber);
        Task<VehicleType> SelectVehicleType(string type);
        Task<ServiceResult<Vehicle>> AddVehicle(Vehicle vehicle);
        Task<ServiceResult<VehicleTollEvent>> SetTollEventVehicle(long tollEventId, long vehicleId);
        Task<Vehicle> SelectVehicleAndEvents(string registrationNumber, DateTime startDate, DateTime endDate);
        Task<IEnumerable<TollFee>> SelectAllTollFees();
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
                await _context.VehicleTollEvent.AddAsync(vehicleTollEvent);
                await _context.SaveChangesAsync();

                return new ServiceResult<VehicleTollEvent>(vehicleTollEvent);
            }
            catch (Exception ex)
            {
                return new ServiceResult<VehicleTollEvent>(ex);
            }
        }

        public async Task<Vehicle> SelectVehicle(string registrationNumber)
        {
            return await _context.Vehicle.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
        }

        public async Task<VehicleType> SelectVehicleType(string type)
        {
            var vehicleType = await _context.VehicleType.FirstOrDefaultAsync(x => x.Type == type);
            if (vehicleType == null)
                vehicleType = await _context.VehicleType.FirstOrDefaultAsync(x => x.Type == "Default");

            return vehicleType;

        }

        public async Task<ServiceResult<Vehicle>> AddVehicle(Vehicle vehicle)
        {
            try
            {
                await _context.Vehicle.AddAsync(vehicle);
                await _context.SaveChangesAsync();

                return new ServiceResult<Vehicle>(vehicle);
            }
            catch (Exception ex)
            {
                return new ServiceResult<Vehicle>(ex);
            }
        }

        public async Task<ServiceResult<VehicleTollEvent>> SetTollEventVehicle(long tollEventId, long vehicleId)
        {
            try
            {
                var entity = await _context.VehicleTollEvent.FindAsync(tollEventId);
                entity.VehicleId = vehicleId;

                await _context.SaveChangesAsync();

                return new ServiceResult<VehicleTollEvent>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceResult<VehicleTollEvent>(ex);
            }
        }

        public async Task<Vehicle> SelectVehicleAndEvents(string registrationNumber, DateTime startDate, DateTime endDate)
        {
            return await _context.Vehicle
                .Where(x => x.RegistrationNumber == registrationNumber)            
                .Include(x => x.VehicleTollEvents.Where(e => e.EventTime > startDate && e.EventTime < endDate))
                .Include(x => x.VehicleType)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TollFee>> SelectAllTollFees()
        {
            return await _context.TollFee.ToListAsync();
        }
    }
}
