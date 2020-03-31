using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
