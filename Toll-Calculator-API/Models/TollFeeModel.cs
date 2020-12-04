using System;

namespace Toll_Calculator_API.Models
{
    public class TollFeeModel
    {
        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Fee { get; set; }

        public VehicleModel Vehicle { get; set; }
    }
}
