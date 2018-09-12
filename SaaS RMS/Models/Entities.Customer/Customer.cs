using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Customer
{
    public class Customer
    {
        #region Model Data

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name field is required")]
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

        public String Address { get; set; }

        public BloodGroup BloodGroup { get; set; } 

        public Genotype Genotype { get; set; }

        public String Allegies { get; set; }

        public string DisplayName
            => FirstName + " " + LastName;

        #endregion

        #region Foreign Key

        //[DisplayName("LGA")]
        //public int LgaId { get; set; }
        //[ForeignKey("LgaId")]
        //public virtual Lga Lga { get; set; }

        #endregion
    }
}
