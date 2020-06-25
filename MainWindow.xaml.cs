using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Runtime.Remoting;

namespace ReView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string RedditType = "pics";
        double ScrollLimit = .80;
        string SourceDirectory = System.IO.Path.GetFullPath(@"..\..\") + "Images";//Was used when grabbing images from local disk, in testing.
        string LastPost;
        bool Loading= false;

        Image Holder;
        BitmapImage BitHolder;
        Uri URIHolder;
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();//Opens a client to access the internet.

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)// Begins logic, after main window opens.
        {
            await LoadImages();
        }

        async void OnClick0(object sender =null, RoutedEventArgs e=null)
        {
            Wrap.Children.Clear(); //Garbage collection handles purging the objects from memory... I think.
            RedditType = Custom.Text;

            try // incase the subreddit being selected doesn't exist. 
            {
                ScrollIt.ScrollToVerticalOffset(0);
                await LoadImages(25, Custom.Text);
                ScrollLimit = .80;

            }
            catch (Exception)// a Popup to inform the user should be displayed here. Maybe even wiggling the button with a red outline.
            {
                RedditType = "pics";// adding this as a reset to a known good value, preventing other possible errors.
            }

        }

        void OnKeyDownHandler(object Sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                this.OnClick0();
            }
        }
        async void OnClick1(object sender, RoutedEventArgs e)
        {
            Wrap.Children.Clear();
            await LoadImages(25, RedditType, "top");
            ScrollIt.ScrollToVerticalOffset(0);
            ScrollLimit = .80;
        }
        async void OnClick2(object sender, RoutedEventArgs e)
        {
            Wrap.Children.Clear();


            await LoadImages(25, RedditType, "new");
            ScrollIt.ScrollToVerticalOffset(0);
            ScrollLimit = .80;
        }
        async void OnClick3(object sender, RoutedEventArgs e)
        {
            Wrap.Children.Clear();
            await LoadImages(25, RedditType, "hot");
            ScrollIt.ScrollToVerticalOffset(0);
            ScrollLimit = .80;
        }

        async void Movement(object sender, RoutedEventArgs e)
        {
            if(ScrollIt.VerticalOffset >= (ScrollIt.ScrollableHeight * ScrollLimit ) && ScrollIt.VerticalOffset !=0 && !Loading )// Checks if user is near at 80 percent of the page. to load more.
            {
                Loading = true;
                await LoadImages(25, RedditType, "hot", LastPost);
                
                ScrollLimit = .90; // After first page it will now start loading more at 90%. May need to write a method to do this further.

                 //This is calling when i want it to, but is calling too frequently. I need to add some sort of flag to prevent this from calling a function a bunch of times.
            }
        }
        void ImageClick(object sender, RoutedEventArgs e) 
        {
            Grid.SetZIndex(CenterFold, 2);
            Console.WriteLine("image has been clicked"); // need to adjust these lines to load the image I want. just placeholder honestly

            Holder = (Image)sender;
            BitHolder = (BitmapImage)Holder.Source;
            URIHolder = BitHolder.UriSource;

            
            BitHolder = new BitmapImage();
            BitHolder.BeginInit();
            BitHolder.UriSource = URIHolder;
            BitHolder.CacheOption = BitmapCacheOption.OnDemand;
            BitHolder.EndInit();

            ZoomImage.Source = BitHolder;
        }

        void ExitFull(object sender, RoutedEventArgs e) {
            Grid.SetZIndex(CenterFold, 0);
        }


        private async Task LoadImages(int number = 25, String Subreddit = "pics", String type = "hot" , String After = null)//Begins to request images from Reddit. Also Loads them into the app. Async threads it.
        {

            ImageModel ImageSet;    //Gets the Json info that contains the URL's to the images we're looking for. Returns an ImageModel class
            if (After == null)
            { ImageSet = await ImageGrabber.LoadImage(Subreddit, type, number); }
            else {
                ImageSet = await ImageGrabber.LoadImage(Subreddit, type, number,After);
            }

            for (int i = 0; i < ImageSet.Data.Dist; i++)// Itterates based on how many images should be in Imageset.
            {
                // if (ImageSet.Data.Children[i].Data.Url == Regex for Png/jpg), some sudo for a filter I should Add. Doesn't seem to be needed honestly, but could mitigate useless web calls.


                Uri MyUri = new Uri(ImageSet.Data.Children[i].Data.Url);//Pulls the URL from the JSON we acquired 


                BitmapImage Mybitty = new BitmapImage(); //Assigns Url to bitmap 
                Mybitty.BeginInit(); //Tunes settings of Bitmap
                Mybitty.UriSource = MyUri;
                Mybitty.CacheOption = BitmapCacheOption.OnDemand;//This should load the images more effeciently.
                Mybitty.DecodePixelWidth = 400;

                Mybitty.EndInit();//finalized settings
                Image ImageSource = new Image();//Creates new image
                ImageSource.Source = Mybitty;//Assigns Bitmap to image.
                
                ImageSource.Tag = ImageSet.Data.Children[i].Data.Name; // grabs the Name of the post. useful for later. May need to store a bunch of shit here.

                // var tangle = new Rectangle,I want rounded corners on images. To do this, I need to make rectangles and load images into them. wrap.children[i].add(image) may work.
                Wrap.Children.Add(ImageSource);//Adds to UI.
            }
            Loading = false;
            LastPost = ImageSet.Data.Children[ImageSet.Data.Dist-1].Data.Name;
        }


        public Image AddImage(string path)// Using this function to adjust setting of bitmap prior to adding image to display. Currently not in use, old version.
        {
            Uri Loc = new Uri(path);
            BitmapImage bitty = new BitmapImage();


            bitty.BeginInit();
            bitty.UriSource = Loc;
            bitty.DecodePixelWidth = 1000; // I should add a public variable that adjusts this, so user can set quality levels.
            bitty.EndInit();


            Image localImage = new Image();
            localImage.Source = bitty;
            return localImage;
        }

        private void Custom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }


}
