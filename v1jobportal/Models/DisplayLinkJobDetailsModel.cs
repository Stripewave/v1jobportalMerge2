using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace v1jobportal.Models
{
    public class DisplayLinkJobDetailsModel
    {
        [Key]
        public string JobTitle { get; set; }
        public string DutyStation { get; set; }
        public string DutiesBrief { get; set; }
        public string Qualification { get; set; }
        public string Deadline { get; set; }
    }
}
