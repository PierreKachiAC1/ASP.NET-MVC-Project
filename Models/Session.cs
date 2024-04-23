using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public int DurationHours { get; set; }
        public DateTime EndDate { get; set; }


        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(200)]
        public string Purpose { get; set; }

        public int? CreatedBy { get; set; }
        public int CreatedByUserId { get; set; }
        public bool Active { get; set; } = true;

        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}