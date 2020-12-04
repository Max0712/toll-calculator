using System;
using System.ComponentModel.DataAnnotations;

namespace Toll_Calculator_API.Models
{
    public class TollEventRegistration
    {
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public string VehicleType { get; set; }
        public DateTime? EventTime { get; set; } = DateTime.Now;
    }
}
