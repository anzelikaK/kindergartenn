using kindergarten.ApplicationDate;
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

namespace kindergarten.PageMain
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            AppFrame.SecondFrame = SecondFrame;
        }

        private void btnKid_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageChild());
        }

        private void btnTeach_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageTeacher());
        }

        private void btnParent_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageParent());
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageGroup());
        }

        private void btnMed_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageMedicalCard());
        }

        private void btnVacc_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageVaccination());
        }

        private void btnVisit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageVisit());
        }
    }
}
