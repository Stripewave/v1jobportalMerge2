
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Utilities
{
    public interface IFormFile
    {
        public interface IFormFile
        {
            public int Id { get; set; }
            [Display(Name = "Job Id")]
            [Required]
            public int? Jd_JobId { get; set; }
            [Required]
            [Display(Name = "Employee Id")]
            public int Emp_Id { get; set; }
            public byte UploadCv { get; set; }

            public byte UploadCoverLetter { get; set; }
        }
    }
}
