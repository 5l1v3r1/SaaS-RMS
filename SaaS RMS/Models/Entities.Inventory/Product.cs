using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Inventory
{
    public class Product
    {
        #region Model Data

        public int ProductId { get; set; }

        [Required(ErrorMessage="Name field is required!!!")]
        public string Name { get; set; }

        public double Quantity { get; set; }

        #endregion

        #region Foreign Keys

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        #endregion


    }
}
