using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class AccountModel
    {
        #region Data Model

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        
        public string LoginType { get; set; }

        public string ProfilePicture { get; set; }

        #endregion

        #region Foreign Key

        public int? RoleId { get; set; }

        #endregion
    }
}
