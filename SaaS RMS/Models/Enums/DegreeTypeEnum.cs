using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Enums
{
    public enum DegreeTypeEnum
    {
        [Display(Name = "Basic Education")]
        Basic,
        [Display(Name = "JSCE")]
        JSCE,
        [Display(Name = "SSCE/WAEC")]
        SSCE,
        [Display(Name = "B.Arch")]
        Ba,
        [Display(Name = "B.A")]
        Barch,
        [Display(Name = "B.B.A")]
        Bba,
        [Display(Name = "B.F.A")]
        Bfa,
        [Display(Name = "B.S")]
        BSc,
        [Display(Name = "L.L.M")]
        Llm,
        [Display(Name = "M.A")]
        Ma,
        [Display(Name = "M.B.A")]
        Mba,
        [Display(Name = "M.Phil")]
        Mphil,
        [Display(Name = "M.Res")]
        Mres,
        [Display(Name = "M.S")]
        MSc,
        [Display(Name = "Phd")]
        Phd,
        Other
    }
}
