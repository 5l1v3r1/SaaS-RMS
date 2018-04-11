using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class PurchaseEntry
    {
        #region Model Data

        public int PurchaseEntryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Name field is required!!!")]
        public string Name { get; set; }

        #endregion

        #region Foreign Key

        [DisplayName("Restaurant")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restuarant { get; set; }

        //[DisplayName("Employee")]
        //public int EmployeeId { get; set; }
        //[ForeignKey("EmployeeId")]
        //public virtual Employee.Employee Employee { get; set; }

        #endregion



    }
}
