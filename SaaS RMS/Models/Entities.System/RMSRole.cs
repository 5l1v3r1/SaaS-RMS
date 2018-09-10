using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class RMSRole
    {
        #region Model Data

        public int RMSRoleId { get; set; }

        public string Name { get; set; }

        public bool CanManageRestaurants { get; set; }

        #endregion
    }
}
