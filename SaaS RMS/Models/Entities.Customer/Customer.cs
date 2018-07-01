using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Customer
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage="First Name field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password field is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number field is required")]
        public string PhoneNumber { get; set; }
    }
}
