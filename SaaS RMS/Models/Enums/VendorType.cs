using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Enums
{
    public enum VendorType
    {
        [Display(Name="Self Created")] Created, Accept, Declined, Registered
    }
}
