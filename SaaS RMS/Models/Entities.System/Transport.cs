using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class Transport
    {
        #region Data Model

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

        [DisplayName("Restaurant Name")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

        #endregion
    }
}
