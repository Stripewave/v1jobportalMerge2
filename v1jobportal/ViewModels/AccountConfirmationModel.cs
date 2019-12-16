using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace v1jobportal.ViewModels
{
    public class AccountConfirmationModel
    {
        [Required]
        public string Email { get; set; }
        
    }
}
