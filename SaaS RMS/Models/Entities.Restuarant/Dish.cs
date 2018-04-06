using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class Dish
    {
        #region Model Data

        [Key]
        public int DishId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public double Amount { get; set; }

        #endregion

        #region Forigen Keys

        [DisplayName("Meal")]
        public int MealId { get; set; }
        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }

        #endregion
    }
}
