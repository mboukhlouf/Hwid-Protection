using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using HwidProtectionServer.Models;

namespace HwidProtectionServer.Extensions
{
    public static class SessionExtensions
    {
        public static object GetObject(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }

            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(data, 0, data.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            return (T)GetObject(session, key);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            byte[] data = null;
            if (value != null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, value);
                    data = ms.ToArray();
                }
            }
            session.Set(key, data);
        }
    }
}
