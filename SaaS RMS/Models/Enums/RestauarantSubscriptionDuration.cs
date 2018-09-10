using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Enums
{
    public enum RestauarantSubscriptionDuration
    {
        [DisplayName("One Month")]
        OneMonth,
        [DisplayName("Six Months")]
        SixMonths,
        [DisplayName("Twelve Months")]
        TwelveMonths
    }
}
