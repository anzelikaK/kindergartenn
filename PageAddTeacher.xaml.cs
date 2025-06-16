using kindergarten.ApplicationDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для PageAddTeacher.xaml
    /// </summary>
    public partial class PageAddTeacher : Page
    {
        private Teacher _currentTeacher = new Teacher();
        public PageAddTeacher()
        {
            InitializeComponent();
            DataContext = _currentTeacher;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentTeacher.Surname))
                errors.AppendLine("Укажите фамилию воспитателя");
            if (string.IsNullOrWhiteSpace(_currentTeacher.NameTeacher))
                errors.AppendLine("Укажите имя воспитателя");
            if (string.IsNullOrWhiteSpace(_currentTeacher.Patronymic))
                errors.AppendLine("Укажите отчество воспитателя");
            if (string.IsNullOrWhiteSpace(_currentTeacher.Speciality))
                errors.AppendLine("Укажите специальность воспитателя");
            if (string.IsNullOrWhiteSpace(_currentTeacher.Email))
                errors.AppendLine("Укажите почту воспитателя");
            if (_currentTeacher.Number <= 0)
                errors.AppendLine("Укажите номер воспитателя");
            if (string.IsNullOrWhiteSpace(_currentTeacher.Education))
                errors.AppendLine("Укажите образование воспитателя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentTeacher.idTeacher == 0)
                kindergartenEntities.GetContext().Teacher.Add(_currentTeacher);

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
            AppFrame.FrameMain.Navigate(new PageTeacher());
        }
    }
    
}
