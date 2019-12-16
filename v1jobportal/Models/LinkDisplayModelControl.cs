using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace v1jobportal.Models
{
    public class LinkDisplayModelControl
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Duty Station")]
        public string DutyStation { get; set; }

        [Display(Name = "Duties")]
        public string DutyDscription { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display (Name = "DeadLine")]
        public string  Deadline { get; set; }
    
}
}
