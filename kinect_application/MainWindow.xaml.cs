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

namespace KINECT_APPLICATION
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public StackPanel MainWindowContent;

        public MainWindow()
        {
            InitializeComponent();

            MainWindowContent = new StackPanel();
            MainWindowContent.HorizontalAlignment = HorizontalAlignment.Center;
            //MainWindowContent.VerticalAlignment = VerticalAlignment.Center;
            MainWindowContent.Children.Add(new LoginUserControl());
            this.Content = MainWindowContent;
        }
    }
}
