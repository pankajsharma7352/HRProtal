using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models.Models
{
    public class ShowEmployeeDetails
    {

   
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }


        [Required]
        public string AddressLine1 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PinCode { get; set; }


        public ShowEmployeeDetails(
            string FirstName, 
            string LastName, 
            string Email, 
            string Password,
            string Phone,
            string Gender,
            string AddressLine1,
            string City,
            string State,
            string PinCode
            )
        {

            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            this.Phone = Phone;
            this.Gender = Gender;
            this.AddressLine1 = AddressLine1;
            this.City = City;
            this.State = State;
            this.PinCode = PinCode;
        }

        public ShowEmployeeDetails()
        {
            
        }
    }
}
