using System;
using System.ComponentModel.DataAnnotations;

namespace Toll_Calculator_API.Models
{
    public class VehicleTollSum
    {
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public decimal Sum { get; set; }
    }
}
