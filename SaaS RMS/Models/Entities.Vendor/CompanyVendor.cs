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

        [Required]
        public string Request { get; set; }

        #endregion
    }
}
