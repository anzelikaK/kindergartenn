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
    /// Логика взаимодействия для PageParent.xaml
    /// </summary>
    public partial class PageParent : Page
    {
        public PageParent()
        {
            InitializeComponent();
            DtGridParent.ItemsSource = kindergartenEntities.GetContext().Parent.ToList();

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            {
                
                if (DtGridParent.SelectedItem == null)
                {
                    MessageBox.Show("Выберите родителя для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show("Удалить выбранного родителя?", "Подтверждение",
                                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                try
                {
                    Parent selectedParent = DtGridParent.SelectedItem as Parent;

                    kindergartenEntities.GetContext().Parent.Remove(selectedParent);
                    kindergartenEntities.GetContext().SaveChanges();

                    DtGridParent.ItemsSource = kindergartenEntities.GetContext().Child.ToList();
                    MessageBox.Show("Родитель удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageAddParent());
        }

        private void Page_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
