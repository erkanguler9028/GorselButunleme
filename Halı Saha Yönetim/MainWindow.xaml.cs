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
using Halı_Saha_Yönetim.PageScreens;

namespace Halı_Saha_Yönetim
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

        public void pageLoad(Page pageLoad)
        {
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            MainFrame.Content = pageLoad;
        }

        private void RezShow_Click(object sender, RoutedEventArgs e)
        {
            ReservationShow reservationShow = new ReservationShow();
            pageLoad(reservationShow);
        }

        private void RezAttempt_Click(object sender, RoutedEventArgs e)
        {
            ReservationAttempt reservationAttempt = new ReservationAttempt();
            pageLoad(reservationAttempt);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            pageLoad(settings);
        }
    }
}
