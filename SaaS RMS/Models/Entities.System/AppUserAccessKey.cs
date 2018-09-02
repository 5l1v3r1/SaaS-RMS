using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
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
