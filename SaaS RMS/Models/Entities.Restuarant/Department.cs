using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class Department
    {
        #region Model Data

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Name field is requried")]
        public string Name { get; set; }

        #endregion

        #region Foreign Keys
        
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        #endregion
    }
}
