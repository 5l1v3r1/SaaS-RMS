using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class RestaurantStatistics
    {
        #region Data Model

        public int RestaurantStatisticsId { get; set; }

        public string Action { get; set; }

        [DisplayName("Date Occured")]
        public DateTime DateOccured { get; set; }

        #endregion

        #region Foreign Keys

        [DisplayName("Restaurant")]
        public int? RestaurantId { get; set; }

        //[DisplayName("Logged In User")]
        //public int? LoggedInUserId { get; set; }

        #endregion


    }
}
