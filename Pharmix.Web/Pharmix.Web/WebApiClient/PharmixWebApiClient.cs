using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PharmixJob.WebApiClient
{
    public class PharmixWebApiClient
    {       
        public HttpClient InitializeClient(string _apiBaseURI)
        {
            //string _apiBaseURI = "http://localhost:49156";
            //string _apiBaseURI = "http://dmd-api.pharmix.co.uk";
            // string tst = _configuration["MySection:MyFirstConfig"]; 
            var client = new HttpClient();
            //Passing service base url  
            client.BaseAddress = new Uri(_apiBaseURI);

            client.DefaultRequestHeaders.Clear();
            //Define request data format  

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(1, 5, 30);
            return client;
        }
    }
}
