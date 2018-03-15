using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Data_Factory
{
    public class StateFactory
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public StateFactory(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        public IEnumerable<Lga> GetLgaForState(int stateId)
        {
            return _db.Lgas.Where(n => n.StateId == stateId);
        }
    }
}
