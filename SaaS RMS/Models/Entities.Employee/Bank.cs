using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class Bank
    {
        #region Model Data

        public int BankId { get; set; }
        public string Name { get; set; }

        #endregion

        #region Foreign Keys

        public int? RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restuarant { get; set; }

        #endregion
    }
}
