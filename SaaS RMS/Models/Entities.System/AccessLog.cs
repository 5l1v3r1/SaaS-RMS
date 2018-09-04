using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.System
{
    public class AccessLog : Transport
    {
        #region Data Model

        public int AccessLogId { get; set; }

        public string Message { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }

        public int? TenancyId { get; set; }

        public int? AppUserId { get; set; }

        #endregion
    }
}
