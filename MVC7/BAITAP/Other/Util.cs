using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BAITAP.Other
{
     public class Util
    {
   
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        public static String GetIpAddress(IHttpContextAccessor _httpContextAccessor)
    {
        string ipAddress;
        try
        {
            ipAddress = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"];

            if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown"))
                ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
        catch (Exception ex)
        {
            ipAddress = "Invalid IP: " + ex.Message;
        }

        return ipAddress;
    }
    }

}
