using AutoMapper;
using Toll_Calculator_API.DbModels;
using Toll_Calculator_API.Models;

namespace Toll_Calculator_API.Mappings
{
    public class VehicleTollProfile : Profile
    {
        public VehicleTollProfile()
        {
            CreateMap<VehicleTypeModel, VehicleType>();
            CreateMap<VehicleType, VehicleTypeModel>();

            CreateMap<VehicleModel, Vehicle>(); 
            CreateMap<Vehicle, VehicleModel>();

            CreateMap<VehicleTollEventModel, VehicleTollEvent>();
            CreateMap<VehicleTollEvent, VehicleTollEventModel>();

            CreateMap<TollFeeModel, TollFee>();
            CreateMap<TollFee, TollFeeModel>();          
        }
    }
}
