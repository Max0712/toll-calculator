using System;
using System.Collections.Generic;

namespace Toll_Calculator_API.Models
{
    public class VehicleTollSummary
    {
        public decimal TotalCost { get; set; } = 0M;
        public IEnumerable<KeyValuePair<string, decimal>> ResultList { get; set; } = new List<KeyValuePair<string, decimal>>();
    }
}
