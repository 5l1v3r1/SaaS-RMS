using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.System;
using System.Collections.Generic;

namespace SaaS_RMS.Models.Entities.Restuarant
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
