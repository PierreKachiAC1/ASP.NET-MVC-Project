using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.ViewModels
{
    public class SessionViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int DurationHours { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(200)]
        public string Purpose { get; set; }
        public bool Active { get; set; } = true;
    }
}
