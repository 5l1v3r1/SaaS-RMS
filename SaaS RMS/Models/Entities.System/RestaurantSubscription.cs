using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class RestaurantSubscription
    {
        #region Data model

        public int RestaurantSubscriptionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [DisplayName("Subscription Duration")]
        public string Duration { get; set; }

        #endregion

        #region Foreign Key

        public int? PackageId { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }

        public int? RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        #endregion
    }
}
