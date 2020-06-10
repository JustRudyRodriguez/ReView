using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ReView
{
    static public class ImageGrabber
    {
        static public async Task<ImageModel> LoadImage(string SubReddit, int amount) // The async allows this to be somewhat threaded. And the Task does somethign like that as well.
        {

            // Uri MyUrl = ApiHelper.ApiClient.BaseAddress; may be unnessecary.

            string myUrl = $"https://www.reddit.com/r/pictures/new.json?limit={amount}";
            Console.WriteLine(myUrl);// just for testing.

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(myUrl))//Makes call to url and awaits response, closes calls after bracket closes.
            {
                if (response.IsSuccessStatusCode)//Checks if reply was a good reply.
                {
                    ImageModel currentImage = await response.Content.ReadAsAsync<ImageModel>();//ReadasAsync-Converts Json Into ImageModel, based on imagemodel properties

                    return currentImage;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);//Gives an error if there is a issue grabbing data.
                }

            }
             
        }
    }
}
