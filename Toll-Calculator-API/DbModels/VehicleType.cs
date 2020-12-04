using System.Collections.Generic;

namespace Toll_Calculator_API.DbModels
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public bool IsFree { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
