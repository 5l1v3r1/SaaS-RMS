using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class Role
    {
        public int RoleId { get; set; }

        [DisplayName("Employee Management")]
        public bool CanManageEmployee { get; set; }

        [DisplayName("Order Management")]
        public bool CanManageOrders { get; set; }
    }
}
