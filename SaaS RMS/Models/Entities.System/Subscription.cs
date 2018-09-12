using SaaS_RMS.Models.Enums;
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
        
        [DisplayName("Subscription Duration")]
        public RestauarantSubscriptionDuration Duration { get; set; }

        public double Amount { get; set; }

        #endregion

        #region Foreign Key

        [DisplayName("Package")]
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }

        #endregion
        
    }
}
