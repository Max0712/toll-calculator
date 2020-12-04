using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toll_Calculator_API.DbModels
{
    public partial class TollFee
    {
        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Fee { get; set; }

        public virtual Vehicle Vehicle { get; set; }

    }
}
