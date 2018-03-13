using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class Lga
    {
        #region Model Data

        public int LgaId { get; set; }

        [Required(ErrorMessage = "Name field is required!!!")]
        public string Name { get; set; }

        #endregion
        
    }
}
