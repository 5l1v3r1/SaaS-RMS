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
    public class EmploymentPosition
    {
        #region Model Data

        public long EmploymentPositionId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Income is required")]
        public long Income { get; set; }

        [DisplayName("Is this position for senior members of your restaurant?")]
        public bool SeniorMember { get; set; }

        #endregion

        #region Foreign Keys

        public long RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        #endregion

        #region IEnumerables

        public IEnumerable<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        //public IEnumerable<PositionChange> PositionChanges { get; set; }

        #endregion
    }
}
