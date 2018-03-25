using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession _session, string key, object value)
        {
            _session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession _session, string key)
        {
            var value = _session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
