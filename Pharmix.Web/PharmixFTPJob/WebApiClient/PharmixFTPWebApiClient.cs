using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PharmixFTPJob.WebApiClient
{
    public class PharmixFTPWebApiClient
    {
      
        public HttpClient InitializeClient()
        {
            string _apiBaseURI = ConfigurationManager.AppSettings["ApiBaseURI"].ToString();           
            var client = new HttpClient();
            //Passing service base url  
            client.BaseAddress = new Uri(_apiBaseURI);

            client.DefaultRequestHeaders.Clear();
            //Define request data format  

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 20, 0);
            return client;
        }
    }
}
