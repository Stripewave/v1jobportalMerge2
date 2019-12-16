using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace v1jobportal.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        [Display(Name = "CV (bytes)")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public long ScheduleCv { get; set; }

        [Display(Name = "Cover Letter (bytes)")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public long ScheduleCoverLeter { get; set; }
    }
}
