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
    /// Логика взаимодействия для PageAddVisit.xaml
    /// </summary>
    public partial class PageAddVisit : Page
    {
        private Visit _currentVisit = new Visit();

        public PageAddVisit()
        {
            InitializeComponent();
            DataContext = _currentVisit;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentVisit.idChild == 0)
                errors.AppendLine("Укажите индекс ребенка");
            if (_currentVisit.Date == null)
                errors.AppendLine("Укажите дату");
            if (string.IsNullOrWhiteSpace(_currentVisit.Status))
                errors.AppendLine("Укажите статус");
           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentVisit.idVisit == 0)
                kindergartenEntities.GetContext().Visit.Add(_currentVisit);

            try
            {
                kindergartenEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.FrameMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageVisit());
        }
    }
}
