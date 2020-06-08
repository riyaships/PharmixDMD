using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Pharmix.Web.Services.Mappers
{

    [DataContract]
    public class RecaptchaApiResponse
    {
        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "error-codes")]
        public List<string> ErrorCodes;
    }
    public class ReCaptchaValidator
    {
        
        public static List<string> ErrorCodes { get; set; } = new List<string>();
        public static bool ValidateCaptcha(HttpRequest request, string _reCaptchaSecret)
        {
            var sb = new StringBuilder();
            sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");
            sb.Append(_reCaptchaSecret);
            sb.Append("&response=");
            sb.Append(request.Form["g-recaptcha-response"]);

            //client ip address
            sb.Append("&remoteip=");
            sb.Append(GetUserIp(request));

            //make the api call and determine validity
            using (var client = new WebClient())
            {
                var uri = sb.ToString();
                var json = client.DownloadString(uri);
                var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

                if (result == null)
                {
                    return false;
                }
                else if (result.ErrorCodes != null)
                {
                    //foreach (var code in result.ErrorCodes)
                    //{
                    //    this.ErrorCodes.Add(code.ToString());
                    //}
                    return false;
                }
                else if (!result.Success)
                {
                    return false;
                }
                else //-- If successfully verified.
                {
                    return true;
                }
            }
        }

        //--- To get user IP(Optional)
        private static string GetUserIp(HttpRequest request)
        {
            return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

    }
}