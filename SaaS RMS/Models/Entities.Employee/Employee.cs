using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class Employee
    {
        #region Model Data

        public int EmployeeId { get; set; }
        public List<EmployeePersonalData> EmployeePersonalDatas { get; set; }

        #endregion

        #region Foreign Keys

        [DisplayName("Restaurant")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        //[DisplayName("Assigned Department")]
        //public int DepartmentId { get; set; }
        //[ForeignKey("DepartmentId"), Required]
        //public virtual Department Department { get; set; }

        //[DisplayName("Assigned Role")]
        //public int RoleId { get; set; }
        //[ForeignKey("RoleId"), Required]
        //public virtual Role Role { get; set; }

        #endregion

        #region IEnumerable

        public IEnumerable<Department> Departments { get; set; }

        #endregion
    }
}
