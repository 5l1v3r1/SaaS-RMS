
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
    public class Meal
    {
        #region Model Data

        public int MealId { get; set; }

        [Required(ErrorMessage = "Name field is required!!!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Image field is required!!!")]
        public string Image { get; set; }

        #endregion

        #region Foreign Keys

        [DisplayName("Restaurant")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        #endregion

        #region IEnumerable

        //public virtual ICollection<Dish> Dishes { get; set; }

        #endregion


    }
}
