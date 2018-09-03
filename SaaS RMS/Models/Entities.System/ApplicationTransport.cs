using SaaS_RMS.Models.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class ApplicationTransport
    {
        #region Data Model

        public List<EmployeePersonalData> EmployeePersonalDatas { get; set; }
        public List<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        public List<Employee.Employee> Employees { get; set; }
        public Restaurant Restaurant { get; set; }

        #endregion
    }
}
