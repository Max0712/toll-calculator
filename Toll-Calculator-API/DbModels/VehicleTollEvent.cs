using System;


namespace Toll_Calculator_API.DbModels
{
    public partial class VehicleTollEvent
    {
        public long Id { get; set; }
        public DateTime EventTime { get; set; }
        public long VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }


    }
}
