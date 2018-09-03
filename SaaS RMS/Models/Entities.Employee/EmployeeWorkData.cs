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
    public class EmployeeWorkData
    {
        #region Model Data

        public int EmployeeWorkDataId { get; set; }

        [DisplayName("Employment Date"), Required(ErrorMessage = "Employment date is requried")]
        public DateTime EmploymentDate { get; set; }

        [DisplayName("Employment Status"), Required(ErrorMessage = "Employment status is requried")]
        public string EmploymentStatus { get; set; }

        [Required]
        public decimal? Income { get; set; }
        [DisplayName("Work Hours Per Day")]
        public long WorkHour { get; set; }
        [DisplayName("Work Days Per Week")]
        public long WorkDays { get; set; }
        [DisplayName("Work Week Per Month")]
        public long WorkWeek { get; set; }
        [DisplayName("Work Month Per Year")]
        public long WorkMonth { get; set; }

        #endregion

        #region Foreign Keys

        //public int EmploymentPosistionId { get; set; }
        //[ForeignKey("EmploymentPosistionId")]
        //public virtual EmploymentPosition EmploymentPosition { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        #endregion
    }
}
