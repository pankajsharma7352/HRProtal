using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models.Models
{
    public class EmployeeLogin
    {
        [Required]
        public string Email_Or_Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
