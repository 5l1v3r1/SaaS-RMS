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

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Contact Number 1")]
        public string ContactNumber { get; set; }

        [Display(Name = "Contact Number 2")]
        public string OfficeNumber { get; set; }

        public string VendorItem { get; set; }
        
        public string Request { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password doenst match")]
        public string ConfirmPassword { get; set; }

        public VendorType VendorType { get; set; }

        #endregion
    }
}
