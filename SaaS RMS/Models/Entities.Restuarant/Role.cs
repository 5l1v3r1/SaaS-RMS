using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class Role
    {
        #region Model Data

        public int RoleId { get; set; }

        public string Name { get; set; }

        [DisplayName("Employee Management")]
        public bool CanManageEmployee { get; set; }

        [DisplayName("Order Management")]
        public bool CanManageOrders { get; set; }

        #endregion

        #region Foreign Keys

        public int? RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restuarant { get; set; }

        #endregion

        #region IEnumerables



        #endregion

    }
}
