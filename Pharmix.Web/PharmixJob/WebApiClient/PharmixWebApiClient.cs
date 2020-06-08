using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PharmixJob.WebApiClient
{
    public class PharmixWebApiClient
    {
        //private string _apiBaseURI = "http://localhost:49156";
        public HttpClient InitializeClient()
        {
            string _apiBaseURI = ConfigurationManager.AppSettings["ApiBaseURI"].ToString();

            //string _apiBaseURI = "http://dmd-api.pharmix.co.uk/";
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
