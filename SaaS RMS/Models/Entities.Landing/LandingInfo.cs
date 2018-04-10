using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Landing
{
    public class LandingInfo
    {
        public int LandingInfoId { get; set; }

        [Required(ErrorMessage="Image field required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "About Us field required")]
        public string AboutUs { get; set; }

        [Required(ErrorMessage = "Offers field required")]
        public string Offers { get; set; }
        
        public ApprovalEnum Approval { get; set; }

    }
}
