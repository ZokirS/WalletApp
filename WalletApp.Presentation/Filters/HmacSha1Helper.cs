using System.Security.Cryptography;
using System.Text;

namespace WalletApp.Presentation.Filters
{
    public static class HmacSha1Helper
    {
        public static string CalculateHmacSha1(string data, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (HMACSHA1 hmac = new HMACSHA1(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
