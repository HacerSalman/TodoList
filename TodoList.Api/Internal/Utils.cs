using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model.Context;
using TodoList.Model.Entities;

namespace TodoList.Api.Internal
{
    public class Utils
    {
        public static string EncodePassword(string password)
        {
            var passwordKey = "Your password key";

            byte[] data = System.Text.Encoding.ASCII.GetBytes(password + passwordKey);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
           return System.Text.Encoding.ASCII.GetString(data);

        }

        public static long GetUnixTimeNow()
        {
             return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static long GetUnixTimeFromDT(DateTime dateTime)
        {
            return (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime GetDateTimeFromUnixTime(long unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
            return dtDateTime;
        }

        public static bool CheckBasicAuth(TodoListDBContext db,StringValues authorizationToken, ref User user_ref)
        {
            if (!String.IsNullOrEmpty(authorizationToken.ToString()))
            {
                string[] decodedCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(authorizationToken.ToString().Replace("Basic ", ""))).Split(new[] { ':' });

                var encodePassword = EncodePassword(decodedCredentials[1]);
                var user = db.User.Where(u => u.UserName == decodedCredentials[0] && u.Password == encodePassword).FirstOrDefault();
                if (user == null)
                    return false;
                else
                {
                    user_ref = user;
                    return true;
                }
                   
            }
            return false;
        }
    }
}
