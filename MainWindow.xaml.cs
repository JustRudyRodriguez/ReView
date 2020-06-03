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
            for (var i = 0; i < 40; i++)
            {
                x = rnd.Next(10,200);
                y = rnd.Next(10,200);
                Wrap.Children.Add(new Rectangle
                {
                    Width = x,
                    Height = y,
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(5)
                });
            }

            Wrap.Children.Add(new Image {            });// Need to add attributes for adding images. 


        }

        private static Random rnd = new Random();
    }
}
