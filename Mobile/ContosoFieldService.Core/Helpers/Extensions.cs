using System;
using System.Security.Cryptography;
using System.Text;

namespace ContosoFieldService.Helpers
{
    public static class Extensions
    {
        public static string EmailToGravatarUrl(string email)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            var hash = sBuilder.ToString();

            return $"https://www.gravatar.com/avatar/{hash}?s=512";
        }
    }
}
