using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReView
{
   static class ApiHelper // allows me to make internet calls, does some setup.
    {
        public static HttpClient ApiClient { get; set; } // We make this static, so that we only open 1 port per application, for security/effeciency.

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();//Kinda acts like a web browser, can support multiple tabs.

            ApiClient.BaseAddress = new Uri("https://www.reddit.com");
            ApiClient.DefaultRequestHeaders.Accept.Clear();//clears header before assigning one.
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));// sets header to ask for only Json Data.




        }
    }
}
