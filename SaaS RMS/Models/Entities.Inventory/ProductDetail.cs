using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class ProductDetail
    {
        #region Data Model

        public int ProductDetailId { get; set; }

        [Display(Name = "Amount Per Unit")]
        [Required(ErrorMessage="Amount field is required!!!")]
        public double Amount { get; set; }

        [Required(ErrorMessage ="Measurement field is required!!!")]
        public string Meaurement { get; set; }

        [Display(Name = "Available Quantity")]
        [Required(ErrorMessage = "Quantity field is required!!!")]
        public double Quantity { get; set; }

        #endregion

        #region Foreign Keys

        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor.Vendor Vendor { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual  Product Product { get; set; }
        
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restuarant { get; set; }

        #endregion
    }
}
