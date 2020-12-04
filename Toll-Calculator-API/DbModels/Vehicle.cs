
using System.Collections.Generic;

namespace Toll_Calculator_API.DbModels
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            TollFees = new HashSet<TollFee>();
        }

        public long Id { get; set; }
        public string RegistrationNumber { get; set; }
        public long VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<TollFee> TollFees { get; set; }

    }
}
