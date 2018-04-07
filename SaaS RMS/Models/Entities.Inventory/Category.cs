using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class Category
    {
        #region Data Model

        public int CategoryId { get; set; }
        
        [Required(ErrorMessage="Name field is required!!!")]
        public string Name { get; set; }

        #endregion

        #region Foreign Keys

        public int? RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual System.Restaurant Restaurant { get; set; }

        #endregion
    }
}
