using SaaS_RMS.Models.Entities.Restuarant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class EmployeeEducationalQualification
    {
        #region Model Data

        public int EmployeeEducationalQualificationId { get; set; }

        [Required]
        [DisplayName("Institution Name")]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Degree Attained")]
        public string DegreeAttained { get; set; }

        [DisplayName("Class Of Degree")]
        public string ClassOfDegree { get; set; }

        [DisplayName("File Upload")]
        public string FileUpload { get; set; }

        #endregion

        #region Foreign Keys

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        
        public int? RestaurantQualificationId { get; set; }
        [ForeignKey("RestaurantQualificationId")]
        public virtual RestaurantQualification RestaurantQualification { get; set; }

        #endregion
    }
}
