using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class Package
    {
        #region Data Model

        public int PackageId { get; set; }

        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount field is required")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Type field is required")]
        public string Type { get; set; }
        
        #endregion
    }
}
