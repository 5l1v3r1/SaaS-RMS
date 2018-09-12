using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaaS_RMS.Models.Entities.System
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

        public static implicit operator LandingInfo(List<LandingInfo> v)
        {
            throw new NotImplementedException();
        }
    }
}
