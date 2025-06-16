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
    /// Логика взаимодействия для PageGroup.xaml
    /// </summary>
    public partial class PageGroup : Page
    {
        public PageGroup()
        {
            InitializeComponent();
            DtGridGroup.ItemsSource = kindergartenEntities.GetContext().Group.ToList();

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageMain.PageListGroup((sender as Button).DataContext as Group));
        }
    }
}
