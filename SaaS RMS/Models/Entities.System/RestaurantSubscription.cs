using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class RestaurantSubscription
    {
        #region Data model

        public int RestaurantSubscriptionId { get; set; }
        
        [DisplayName("Subscription Duration")]
        public RestauarantSubscriptionDuration Duration { get; set; }

        [Required(ErrorMessage = "Amount field is required")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Discount field is required")]
        public double Discount { get; set; }

        [DisplayName("Created By")]
        public int? CreatedBy { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Date Last Modified")]
        public DateTime DateLastModified { get; set; }

        [DisplayName("Last Modified By")]
        public int LastModifiedBy { get; set; }

        #endregion

        #region Foreign Key

        public int? PackageId { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        
        #endregion
    }
}
