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

namespace Stock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {   // Products
            Products pro = new Products();
            // pro.Mdiparent = this;
            pro.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {   // Stock

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {   // Report

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            // Application.Current.MainWindow.Close();
        }
    }
}
