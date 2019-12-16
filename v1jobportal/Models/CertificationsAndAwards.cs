using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.Models
{
    public class CertificationsAndAwards
    {
        public int Id { get; set; }
        [Required]
        public string CertificationName { get; set; }
        [Required]
        public string CertifyingInstitutionName { get; set; }

        [DataType(DataType.Date)]
        public DateTime CertificationDate { get; set; }
    }
}
