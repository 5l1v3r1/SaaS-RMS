using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class EmployeeFamilyData
    {
        #region Model Data

        public int EmployeeFamilyDataId { get; set; }

        [DisplayName("Full Name"), Required(ErrorMessage = "Full name of next of kin is required")]
        [StringLength(10, ErrorMessage = "Full name of next of kin is required")]
        public string NextOfKin { get; set; }

        [DisplayName("Address"), Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayName("Contact Number"), Required(ErrorMessage = "Contact number is required")]
        public string ContactNumber { get; set; }

        [DisplayName("Email"), Required(ErrorMessage = "Email is requred")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Date Of Birth"), Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DOB { get; set; }

        [DisplayName("Relationship"), Required(ErrorMessage = "Date Of Birth is required")]
        public string Relationship { get; set; }

        #endregion

        #region Foreign Keys

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        #endregion
    }
}
