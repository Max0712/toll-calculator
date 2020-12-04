using System.Collections.Generic;

namespace Toll_Calculator_API.Models
{
    public class VehicleModel
    {
        public long Id { get; set; }
        public string RegistrationNumber { get; set; }
        public long VehicleTypeId { get; set; }

        public VehicleTypeModel VehicleType { get; set; }
        public IEnumerable<TollFeeModel> TollFees { get; set; }
    }
}
