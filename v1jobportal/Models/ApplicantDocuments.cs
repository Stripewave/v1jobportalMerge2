using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace v1jobportal.Models
{
    public class ApplicantDocuments
     {
         public int Id { get; set; }
        [Display(Name = "Job Id", AutoGenerateFilter = false)]
        [Required]
        public int? Jd_JobId { get; set; }
        [Required]
        [Display(Name = "Employee Id", AutoGenerateFilter = false)]
        public int Emp_Id { get; set; }
        public byte UploadCv { get; set; }

         public byte UploadCoverLetter { get; set; }
    }

    
}
