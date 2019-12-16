using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Models
{
    public class EmploymentHistoryModel
    {
        public int Id { get; set; }
        [Required]
        public string EmployerName { get; set; } 

        [DataType(DataType.Date)]
        public DateTime EmploymentStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EmploymentEndDate { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string SummaryOfDuties { get; set; }
        public string SummaryOfAchievments { get; set; }
    }
}
