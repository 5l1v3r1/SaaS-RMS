using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;
using System.Collections.Generic;

namespace SaaS_RMS.Models
{
    public class DashboardViewModel
    {
        public Restaurant Restaurant { get; set; }

        public List<Meal> Meals { get; set; }

        public List<Dish> Dishes { get; set; }

        public LandingInfo LandingInfo { get; set; }
    }
}
