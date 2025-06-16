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
    /// Логика взаимодействия для PageVaccination.xaml
    /// </summary>
    public partial class PageVaccination : Page
    {
        public PageVaccination()
        {
            InitializeComponent();
            DtGridVaccination.ItemsSource = kindergartenEntities.GetContext().Vaccination.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
