using System;
using System.IO;
using System.Net;
using System.Web;

namespace OnlineBrandingSystem.Classes
{
    public class GetIP
    {
        public string GetUserIP()
        {
            string userIP = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] ??
                             HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(userIP) &&
                !string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress) &&
                (HttpContext.Current.Request.UserHostAddress != "::1" || HttpContext.Current.Request.UserHostAddress != "localhost"))
            {
                userIP = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(userIP))
            {
                userIP = GetIPFromExternalServer();
            }

            return userIP;
        }

        private string GetIPFromExternalServer()
        {
            string externalServerIP = string.Empty;

            try
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    externalServerIP = sr.ReadToEnd();
                }

                int startIndex = externalServerIP.IndexOf("Address: ") + 9;
                int endIndex = externalServerIP.LastIndexOf("</body>");
                externalServerIP = externalServerIP.Substring(startIndex, endIndex - startIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting external server IP: {ex.Message}");
            }

            return externalServerIP;
        }
    }
}
