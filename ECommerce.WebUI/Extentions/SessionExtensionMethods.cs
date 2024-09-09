using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using System.Collections;

namespace ECommerce.WebUI.Extentions
{
    public static class SessionExtensionMethods
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }

        public static T? GetObject<T>(this ISession session, string key) where T : class
        {
            string objectSting = session.GetString(key);
            if (string.IsNullOrEmpty(objectSting))
            {
                return null;
            }
            T result = JsonConvert.DeserializeObject<T>(objectSting);
            return result;
        }

    }
}
