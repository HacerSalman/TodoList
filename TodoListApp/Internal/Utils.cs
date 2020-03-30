using System;
using System.Collections.Generic;
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
    }
}
