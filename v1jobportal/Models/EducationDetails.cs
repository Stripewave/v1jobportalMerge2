using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Models
{
    public class EducationDetails
    {
        public int Id { get; set; }
        public string NameOfInstitution { get; set; }
        public string QualificationAttained { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateQualificationWasAttained { get; set; }
    }
}
