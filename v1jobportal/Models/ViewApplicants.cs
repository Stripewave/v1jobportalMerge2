using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Models
{
    public class ViewApplicants
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Required]
        [Display(Name = "Duty Station")]
        public string DutyStation { get; set; }
        [Required]
        [Display(Name = "Duties Brief")]
        public string Duties { get; set; }

        [Display(Name = "Doctorate Degree")]
        public bool Doctorate { get; set; }
        [Display(Name = "Masters Degree")]
        public bool MastersDegree { set; get; }
        [Display(Name = "Bachelor's Degree")]
        public bool bachelorsDegree { set; get; }
        [Display(Name = "Diploma")]
        public bool Diploma { set; get; }
        [Display(Name = "Certificate")]
        public bool Certificate { set; get; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
    }
}
