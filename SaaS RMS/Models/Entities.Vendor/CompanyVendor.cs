using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Vendor
{
    public class CompanyVendor
    {
        #region Model Data

        public int CompanyVendorId { get; set; }

        [Required(ErrorMessage = "Name field is required!!!")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Address field is required!!!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Display(Name = "Contact Number 1")]
        public string ContactNumber { get; set; }

        [Display(Name = "Contact Number 2")]
        public string OfficeNumber { get; set; }

        [Required(ErrorMessage ="Field is required!!!")]
        public string VendorItem { get; set; }
        
        public string Request { get; set; }

        [Required(ErrorMessage ="Password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password doenst match")]
        public string ConfirmPassword { get; set; }

        public VendorType VendorType { get; set; }

        #endregion
    }
}
