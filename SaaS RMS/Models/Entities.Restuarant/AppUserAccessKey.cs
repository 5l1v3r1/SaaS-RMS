using SaaS_RMS.Models.Entities.System;
using System;

namespace SaaS_RMS.Models.Entities.Restuarant
{
    public class AppUserAccessKey : Transport
    {
        #region Model Data

        public int AppUserAccessKeyId { get; set; }
        public int AppUserId { get; set; }
        public string PasswordAccessCode { get; set; }
        public string AccountActivationAccessCode { get; set; }
        public DateTime? ExpiryDate { get; set; }

        #endregion
    }
}
