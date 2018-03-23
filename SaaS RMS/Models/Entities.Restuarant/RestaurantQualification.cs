using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class RestaurantQualification
    {
        #region Model Data

        public int RestaurantQualificationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        #endregion

        #region Foreign Key

        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        #endregion

        #region IEnumerable

        public IEnumerable<EmployeeEducationalQualification> EducationalQualifications { get; set; }

        #endregion
    }
}
