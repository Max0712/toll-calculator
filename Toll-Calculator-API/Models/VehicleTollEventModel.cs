using System;

namespace Toll_Calculator_API.Models
{
    public class VehicleTollEventModel
    {
        public long Id { get; set; }
        public DateTime EventTime { get; set; }
        public string RegistrationNumber { get; set; }

        public VehicleModel Vehicle { get; set; }
    }
}
