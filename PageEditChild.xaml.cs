
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
    /// Логика взаимодействия для PageEditChild.xaml
    /// </summary>
    public partial class PageEditChild : Page
    {
        private Child _currentChild;
        private List<string> Genders = new List<string> { "М", "Ж" };
        public PageEditChild(Child selectedChild = null)
        {
            InitializeComponent();
            _currentChild = selectedChild ?? new Child();
            DataContext = _currentChild;

            // Инициализация ComboBox для пола
            cmbGender.ItemsSource = Genders;

            // Если передан существующий ребенок - заполняем поля
            if (selectedChild != null)
            {
                cmbGender.SelectedItem = selectedChild.Gender;
            }
            else
            {
                cmbGender.SelectedIndex = 0; // Выбираем первый пол по умолчанию
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            _currentChild.Gender = cmbGender.SelectedItem?.ToString();

            // Парсим ID родителей и мед.карты
            if (!int.TryParse(txtParentId.Text, out int parentId))
            {
                MessageBox.Show("Введите корректный ID родителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _currentChild.idParent = parentId;

            if (!int.TryParse(txtMedicalCardId.Text, out int medicalCardId))
            {
                MessageBox.Show("Введите корректный ID медицинской карты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _currentChild.idMedicalCard = medicalCardId;

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentChild.Surname))
                errors.AppendLine("Укажите фамилию ребенка");
            if (string.IsNullOrWhiteSpace(_currentChild.NameChild))
                errors.AppendLine("Укажите имя ребенка");
            if (string.IsNullOrWhiteSpace(_currentChild.Patronymic))
                errors.AppendLine("Укажите отчество ребенка");
            if (_currentChild.DateOfBirth == null)
                errors.AppendLine("Укажите дату рождения ребенка");
            if (string.IsNullOrWhiteSpace(_currentChild.Gender))
                errors.AppendLine("Укажите пол ребенка");
            if (_currentChild.idParent <= 0)
                errors.AppendLine("Заполните поле родителя");
            if (_currentChild.idGroup <= 0)
                errors.AppendLine("Заполните поле группы");
            if (_currentChild.idMedicalCard <= 0)
                errors.AppendLine("Заполните поле медицинской карты");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var context = kindergartenEntities.GetContext();

                if (_currentChild.idСhild == 0)
                {
                    context.Child.Add(_currentChild);
                }
                else
                {
                    context.Entry(_currentChild).State = System.Data.Entity.EntityState.Modified;
                }

                context.SaveChanges();
                MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.FrameMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}\n\n{ex.InnerException?.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }
    }
    
}
