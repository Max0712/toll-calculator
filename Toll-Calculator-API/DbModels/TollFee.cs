using System;

namespace Toll_Calculator_API.DbModels
{
    public partial class TollFee
    {
        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Fee { get; set; }

    }
}
