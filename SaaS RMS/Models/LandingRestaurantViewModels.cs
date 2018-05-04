using SaaS_RMS.Models.Entities.Landing;
using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models
{
    public class LandingRestaurantViewModels
    {
        public LandingInfo LandingInfo { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
