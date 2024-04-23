using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.ViewModels
{
    public class AttendanceViewModel
    {

        [Required]
        public int SessionId { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Present")]
        public bool Present { get; set; }

        public DateTime AttendanceDateTime { get; set; }=DateTime.Now;

        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }


    }
}
