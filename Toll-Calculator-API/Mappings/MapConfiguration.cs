using AutoMapper;

namespace Toll_Calculator_API.Mappings
{
    public class MapConfiguration
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VehicleTollProfile());
            });
        }
    }    
}
