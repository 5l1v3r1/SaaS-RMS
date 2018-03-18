using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class EmployeeBankData
    {
        #region Model Data

        public int EmployeeBankDataId { get; set; }
        
        [Required]
        [DisplayName("Firstname")]
        public string AccountFirstName { get; set; }

        [DisplayName("Middlename")]
        public string AccountMiddleName { get; set; }

        [Required]
        [DisplayName("Lastname")]
        public string AccountLastName { get; set; }

        [Required]
        [DisplayName("Account Number")]
        [StringLength(11)]
        public string AccountNumber { get; set; }

        [Required]
        [DisplayName("Account Type")]
        public string AccountType { get; set; }

        //public int FakeId { get; set; }

        #endregion

        #region Foreign Keys

        [DisplayName("Bank Name")]
        public int BankId { get; set; }
        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        public string DisplayName
      => AccountFirstName + " " + AccountLastName;
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        #endregion
    }
}
