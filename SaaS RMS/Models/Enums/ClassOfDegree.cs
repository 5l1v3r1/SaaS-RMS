using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Enums
{
    public enum ClassOfDegree
    {
        [DisplayName("First Class")]
        FirstClass,

        [DisplayName("Second Class Upper")]
        SecondClassUpper,

        [DisplayName("Second Class Lower")]
        SecondClassLower,

        [DisplayName("Third Class")]
        ThirdClass,

        [DisplayName("Pass")]
        Pass

    }
}
