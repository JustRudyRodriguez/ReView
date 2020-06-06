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
        public MainWindow()
        {
            InitializeComponent();

            double x;
            double y;
            string SourceDirectory = System.IO.Path.GetFullPath(@"..\..\")+"Images";//Gets the path I actually want.
            var ImagesList = Directory.EnumerateFiles(SourceDirectory);//I can add a search filter here, to only accept specific file types. (SourceDirectory, "*.png") only takes pngs.

            Console.WriteLine(SourceDirectory);


            // should add threading for this section.
            for (var i = 0; i < 40; i++)
            {
                x = rnd.Next(30,200);
                y = rnd.Next(30,200);
                Wrap.Children.Add(new Rectangle
                {
                    Width = x,
                    Height = y,
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(5)
                });
            }
           // Going to use a for each {elem} to put all images from the folder into the app. to see what happens.
            foreach (string path in ImagesList)
             {
                Wrap.Children.Add(AddImage(path));
            }


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
 
    public class iBox// using this to create images to push into the wrap above.
    {
       
    }

}
