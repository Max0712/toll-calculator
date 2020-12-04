using System.Collections.Generic;

namespace Toll_Calculator_API.Models
{
    public class VehicleTollSummary
    {
        public int TotalCost { get; set; } = 0;
        public IEnumerable<KeyValuePair<string, int>> DaySummary { get; set; } = new List<KeyValuePair<string, int>>();
    }
}
