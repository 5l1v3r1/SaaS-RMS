using System.ComponentModel.DataAnnotations;

namespace SaaS_RMS.Models.Entities.Restuarant
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
