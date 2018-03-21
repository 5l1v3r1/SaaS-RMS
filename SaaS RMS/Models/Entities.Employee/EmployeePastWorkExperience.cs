using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class EmployeePastWorkExperience
    {
        #region Model Data

        public int EmployeePastWorkExperienceId { get; set; }

        [DisplayName("Employer Name")]
        [Required]
        public string EmployerName { get; set; }

        [Required]
        [DisplayName("Employer Location")]
        public string EmployerLocation { get; set; }

        [DisplayName("Employer Contact Number")]
        [Required]
        public string EmployerContact { get; set; }

        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        
        [DisplayName("End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        [DisplayName("Reason for leaving")]
        [Required]
        public string ReasonForLeaving { get; set; }

        [DisplayName("Position Held")]
        [Required]
        public string PositionHeld { get; set; }
        
        
        #endregion

        #region Foreign Keys

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        #endregion
    }
}
