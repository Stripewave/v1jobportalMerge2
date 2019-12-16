using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Models
{
    public class ApplicantSummary
    {
        public int Id { get; set; }

        [MinLength(5)]
        [StringLength(1024)]
        public string ApplicantSummaryData { get; set; }
    }
}
