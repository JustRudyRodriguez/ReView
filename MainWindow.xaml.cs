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

namespace ReView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        string SourceDirectory = System.IO.Path.GetFullPath(@"..\..\") + "Images";//Gets the path I actually want.
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();





        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImages();
        }

        private async Task LoadImages(int number = 25)
        {
            var MyImage = await ImageGrabber.LoadImage("pictures", number);
                   
            Uri MyUri = new Uri(MyImage.Data.Children[0].Data.Url);

            BitmapImage Mybitty = new BitmapImage(MyUri);
            Image ImageSource = new Image();
            ImageSource.Source = Mybitty;

            Wrap.Children.Add(ImageSource);
            
        }
        private static Random rnd = new Random();


        public Image AddImage(string path)// Using this function to adjust setting of bitmap prior to adding image to display.
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
