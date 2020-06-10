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

            if (amount > 100){ //reddit has a cap of 100 requests.
                amount = 100;
            }

            string myUrl = $"https://www.reddit.com/r/{SubReddit}/new.json?limit={amount}"; //implements the subreddit and poll amount into url for call.
            Console.WriteLine("Pinged Url is: " + myUrl);// just for testing.

            //By using "using" we can ensure that the port closes after it completes the call to the website.
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(myUrl))//Makes an async GET request to the URL, and stores it as a HTTP response message.
            {
                if (response.IsSuccessStatusCode)//Checks status code from response for a OK.
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
