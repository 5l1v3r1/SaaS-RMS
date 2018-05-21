using SaaS_RMS.Models.Entities.Landing;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models
{
    public class DashboardViewModel
    {
        public Restaurant Restaurant { get; set; }

        public Meal Meals { get; set; }

        public LandingInfo LandingInfo { get; set; }
    }
}
