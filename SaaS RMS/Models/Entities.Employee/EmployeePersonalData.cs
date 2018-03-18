using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class EmployeePersonalData
    {
        #region Model Data

        public int EmployeePersonalDataId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DisplayName("Place Of Birth"), Required(ErrorMessage = "Place Of Birth is required")]
        public string POB { get; set; }

        [DisplayName("Date Of Birth"), Required(ErrorMessage = "Date Of Birth is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public string DOB { get; set; }

        [DisplayName("Primary Address"), Required(ErrorMessage = "Primary Address is required")]
        public string PrimaryAddress { get; set; }

        [DisplayName("Secondary Address")]
        public string SecondaryAddress { get; set; }

        [Required]
        public string Gender { get; set; }

        [DisplayName("Home Phone"), Required(ErrorMessage = "Home Phone is required")]
        public string HomePhone { get; set; }

        [DisplayName("Mobile Number")]
        public string MobilePhone { get; set; }

        [DisplayName("Work Phone"), Required(ErrorMessage = "Work Phone is required")]
        public string WorkPhone { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Marital Status"), Required(ErrorMessage = "Marital Status is required")]
        public string MaritalStatus { get; set; }

        [DisplayName("Image")]
        public string EmployeeImage { get; set; }

        public string DisplayName
            => FirstName + " " + LastName;

        #endregion
    }
}
