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

        #endregion

        #region Foreign Keys

        //public int EmploymentPosistionId { get; set; }
        //[ForeignKey("EmploymentPosistionId")]
        //public virtual EmploymentPosition EmploymentPosition { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        #endregion
    }
}
