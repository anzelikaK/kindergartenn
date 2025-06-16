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
    /// Логика взаимодействия для PageMedicalCard.xaml
    /// </summary>
    public partial class PageMedicalCard : Page
    {
        private MedicalCard _currentMedicalCard;
        public PageMedicalCard(MedicalCard medicalCard = null)
        {
            InitializeComponent();
            _currentMedicalCard = medicalCard;

            if (_currentMedicalCard != null)
            {
                // Загружаем данные конкретной медицинской карты
                LoadMedicalCardData();
            }
            else
            {
                // Загружаем все медицинские карты (старое поведение)
                DtGridMedicalCard.ItemsSource = kindergartenEntities.GetContext().MedicalCard.ToList();
            }
        }

        private void LoadMedicalCardData()
        {
            try
            {
                // Загружаем данные конкретной медицинской карты с информацией о ребенке
                var medicalCardWithChild = kindergartenEntities.GetContext().MedicalCard
                    
                    .FirstOrDefault(m => m.IdMedicalCard == _currentMedicalCard.IdMedicalCard);

                if (medicalCardWithChild == null)
                {
                    MessageBox.Show("Медицинская карта не найдена", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создаем список из одной карты для отображения в DataGrid
                var medicalCardsList = new List<MedicalCard> { medicalCardWithChild };
                DtGridMedicalCard.ItemsSource = medicalCardsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            {
                if (DtGridMedicalCard.SelectedItem == null)
                {
                    MessageBox.Show("Выберите медицискую карту для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show("Удалить выбраную медицинскую карту?", "Подтверждение",
                                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                try
                {
                    MedicalCard selectedMedicalCard = DtGridMedicalCard.SelectedItem as MedicalCard;

                    kindergartenEntities.GetContext().MedicalCard.Remove(selectedMedicalCard);
                    kindergartenEntities.GetContext().SaveChanges();

                    
                    DtGridMedicalCard.ItemsSource = kindergartenEntities.GetContext().MedicalCard.ToList();
                    MessageBox.Show("Медицинская карта удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageAddMedicalCard());
        }
    }
}
