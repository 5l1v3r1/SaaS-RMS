using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class PurchaseOrder
    {
        #region Model Data

        public int PurchaseOrderId { get; set; }

        [Required(ErrorMessage="Select a payment mode!!!")]
        [Display(Name="Payment Mode")]
        public PaymentMode Mode { get; set; }

        [Required(ErrorMessage ="Payment field is required!!!")]
        public double Payment { get; set; }

        [Required(ErrorMessage = "Balance field is required!!!")]
        public double Balance { get; set; }

        [Required(ErrorMessage ="Description field is required!!!")]
        public string Description { get; set; }

        public double Quantity { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        
        [Display(Name ="Total Price")]
        public double TotalPrice { get; set; }

        #endregion

        #region Foreign Key
        
        [Display(Name = "Purchase Entry")]
        public int PurchaseEntryId { get; set; }
        [ForeignKey("PurchaseEntryId")]
        public virtual OrderEntry OrderEntry { get; set; }

        [Display(Name = "Product Detail")]
        public int ProductDetailId { get; set; }
        [ForeignKey("ProductDetailId")]
        public virtual ProductDetail ProductDetail { get; set; }

        #endregion
    }
}
