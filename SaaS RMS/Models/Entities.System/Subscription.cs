using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class Subscription
    {
        #region Model Data

        public int SubscriptionId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        #endregion

        #region Foreign Key

        [DisplayName("Restaurant")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        [DisplayName("Package")]
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }

        #endregion
        
    }
}
