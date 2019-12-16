using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Models
{
    public class ApplicantBioData
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int JobId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        public string Residence { get; set; }
        
        [Required(ErrorMessage = "A 10 digit phone number is Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        
        public string PrimaryPhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        [Required(ErrorMessage = "Atleast one Language is required")]
        public Language LanguagesSpoken { get; set; }
        [Display(Name = " Have you worked with IDRC before ?")]
        public bool WorkedWithIDRC { get; set; }
    }
}
