using SaaS_RMS.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaS_RMS.Models.Entities.Vendor
{
    public class Vendor
    {
        #region Model Data

        public int VendorId { get; set; }

        [Required(ErrorMessage="Name field is required!!!")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Address field is required!!!")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Contact number is required")]
        [Display(Name="Contact Number 1")]
        public string ContactNumber { get; set; }
        
        [Display(Name = "Contact Number 2")]
        public string OfficeNumber { get; set; }

        public string VendorItem { get; set; }

        public VendorType VendorType { get; set; }

        #endregion

        #region Foreign Keys

        public int? RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restuarant { get; set; }
        
        #endregion
    }
}
