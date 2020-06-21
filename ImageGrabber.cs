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
        static public async Task<ImageModel> LoadImage(string SubReddit,string Type, int amount) // The async allows this to be somewhat threaded. And the Task does somethign like that as well.
        {

            if (amount > 100){ //reddit has a cap of 100 requests.
                amount = 100;
            }

            string myUrl = $"https://www.reddit.com/r/{SubReddit}/{Type}.json?limit={amount}"; //implements the subreddit and poll amount into url for call.
            Console.WriteLine("Pinged Url is: " + myUrl);// just for testing.

            //By using "using" we can ensure that the port closes after it completes the call to the website.
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(myUrl))//Makes an async GET request to the URL, and stores it as a HTTP response message.
            {
                if (response.IsSuccessStatusCode)//Checks status code from response for a OK.
                {
                    ImageModel ImageJson= await response.Content.ReadAsAsync<ImageModel>();//ReadasAsync-Converts Json Into ImageModel, based on imagemodel properties
                    Console.WriteLine(ImageJson.message);// Need to further test this. But trying to grab a fake 404 here when reddit isn't found.

<<<<<<< HEAD
                    return ImageJson;
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                    throw new Exception(response.ReasonPhrase);//Gives an error if there is a issue grabbing data.

=======
                    if (currentImage.Data.dist == 0)// checks for false positive 404's.
                    {
                        Console.WriteLine("Subreddit Not found");// for testing.
                        throw new ArgumentNullException();
                    }
                    else
                    {
                        return currentImage;
                    }


                }
                else
                {
                    throw new Exception(response.ReasonPhrase);//Gives an error if there is a issue grabbing data. like 404.
>>>>>>> d4b1e6009d9e57353b395527eeb9589010e24370
                }

            }
             
        }
        static public async Task<ImageModel> LoadImage(string Subreddit, string Type, int amount, string after) {

            // similar to function above, but for loading past the first page.
            if (amount > 100)
            { //reddit has a cap of 100 requests.
                amount = 100;
            }

            string myUrl = $"https://www.reddit.com/r/{Subreddit}/{Type}.json?limit={amount}&after={after}";
            Console.WriteLine("Pinged Url is: " + myUrl);

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(myUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ImageModel ImageJson = await response.Content.ReadAsAsync<ImageModel>();
                    Console.WriteLine(ImageJson.message);

                    return ImageJson;
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                    throw new Exception(response.ReasonPhrase);

                }

            }


        }

    }
}
