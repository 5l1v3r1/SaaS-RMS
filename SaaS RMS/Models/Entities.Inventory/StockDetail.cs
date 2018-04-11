using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class StockDetail
    {
        #region Data Model

        public int StockDetailId { get; set; }
        
        [Required(ErrorMessage="Amount field is required!!!")]
        public double Amount { get; set; }

        [Required(ErrorMessage ="Measurement field is required!!!")]
        public string Meaurement { get; set; }

        #endregion

        #region Foreign Keys
        
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual  Product Product { get; set; }
        
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restuarant { get; set; }

        #endregion
    }
}
