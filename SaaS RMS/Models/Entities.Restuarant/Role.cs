using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant 
{
    public class Role : Transport
    {
        #region Model Data

        public int RoleId { get; set; }

        public string Name { get; set; }

        [DisplayName("Employee Management")]
        public bool CanManageEmployee { get; set; }

        [DisplayName("Order Management")]
        public bool CanManageOrders { get; set; }

        [DisplayName("Can Do Something")]
        public bool CanDoSomething { get; set; }

        #endregion

        #region Foreign Keys

        

        #endregion

        #region IEnumerables



        #endregion

    }
}
