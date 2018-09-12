using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class RestaurantSubscription : Transport
    {
        #region Data model

        public int RestaurantSubscriptionId { get; set; }
        
        [DisplayName("Subscription Duration")]
        public RestauarantSubscriptionDuration Duration { get; set; }
        
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        [Required(ErrorMessage = "Amount field is required")]
        public double Amount { get; set; }

        #endregion

        #region Foreign Key

        public int? PackageId { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        
        #endregion
    }
}
