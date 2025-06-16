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
    /// Логика взаимодействия для PageAddParent.xaml
    /// </summary>
    public partial class PageAddParent : Page
    {
        private Parent _currentParent = new Parent();
        private Parent parent;

        public PageAddParent()
        {
            InitializeComponent();
            DataContext = _currentParent;
        }

        public PageAddParent(Parent parent)
        {
            this.parent = parent;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentParent.Surname))
                errors.AppendLine("Укажите фамилию родителя");
            if (string.IsNullOrWhiteSpace(_currentParent.NameParent))
                errors.AppendLine("Укажите имя родителя");
            if (string.IsNullOrWhiteSpace(_currentParent.Patronymic))
                errors.AppendLine("Укажите отчество родителя");
            if (string.IsNullOrWhiteSpace(_currentParent.Gender))
                errors.AppendLine("Укажите пол родителя");
            if (string.IsNullOrWhiteSpace(_currentParent.Email))
                errors.AppendLine("Укажите почту родителя");
            if (_currentParent.Number <= 0) 
                errors.AppendLine("Введите номер телефона родителя");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentParent.idParent == 0)
                kindergartenEntities.GetContext().Parent.Add(_currentParent);

            try
            {
                kindergartenEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.SecondFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }
    }
    
}
