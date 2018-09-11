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
