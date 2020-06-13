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

        string SourceDirectory = System.IO.Path.GetFullPath(@"..\..\") + "Images";//Was used when grabbing images from local disk, in testing.
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();//Opens a client to access the internet.

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)// Begins logic, after main window opens.
        {
            await LoadImages();
        }

        async void OnClick0(object sender, RoutedEventArgs e) {
            Wrap.Children.Clear(); //Garbage collection handles purging the objects from memory... I think.
            RedditType = Custom.Text;
            await LoadImages(25, Custom.Text);
        
        }
        async void OnClick1(object sender, RoutedEventArgs e)
        {
            Wrap.Children.Clear();
            await LoadImages(25,RedditType,"top");

        }
        async void OnClick2(object sender, RoutedEventArgs e)
        {
            Wrap.Children.Clear();
            await LoadImages(25, RedditType, "new");
        }
        async void OnClick3(object sender, RoutedEventArgs e)
        {
            Wrap.Children.Clear();
            await LoadImages(25, RedditType, "hot");
        }

        private async Task LoadImages(int number = 25,String Subreddit = "pics", String type = "hot")//Begins to request images from Reddit. Also Loads them into the app. Async threads it.
        {
            var ImageSet = await ImageGrabber.LoadImage(Subreddit,type, number);//Gets the Json info that contains the URL's to the images we're looking for. Returns an ImageModel class

            for (int i = 0; i < number; i++)// Itterates based on how many images should be in Imageset.
            {
               // if (ImageSet.Data.Children[i].Data.Url == Regex for Png/jpg), some sudo for a filter I should Add.
                
                Uri MyUri = new Uri(ImageSet.Data.Children[i].Data.Url);//Pulls the URL from the JSON we acquired 


                BitmapImage Mybitty = new BitmapImage(); //Assigns Url to bitmap 
                Mybitty.BeginInit(); //Tunes settings of Bitmap
                Mybitty.UriSource = MyUri;
                Mybitty.CacheOption = BitmapCacheOption.OnDemand;//This should load the images more effeciently.
                Mybitty.DecodePixelWidth = 400;

                Mybitty.EndInit();//finalized settings
                Image ImageSource = new Image();//Creates new image
                ImageSource.Source = Mybitty;//Assigns Bitmap to image.

                Wrap.Children.Add(ImageSource);//Adds to UI.
            }

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
    }
 

}
