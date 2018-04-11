using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class Purchase
    {
        #region Model Data

        public int PurchaseId { get; set; }

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
        public virtual PurchaseEntry PurchaseEntry { get; set; }

        [Display(Name = "Stock Detail")]
        public int StockDetailId { get; set; }
        [ForeignKey("StockDetailId")]
        public virtual StockDetail StockDetail { get; set; }

        #endregion
    }
}
