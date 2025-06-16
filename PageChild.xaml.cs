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
    /// Логика взаимодействия для PageChild.xaml
    /// </summary>
    public partial class PageChild : Page
    {
        public PageChild()
        {
            InitializeComponent();
            DtGridChild.ItemsSource = kindergartenEntities.GetContext().Child.ToList();
            AppFrame.FrameMain.Navigated += FrameMain_Navigated;
        }
        private void FrameMain_Navigated(object sender, NavigationEventArgs e)
        {
            DtGridChild.ItemsSource = kindergartenEntities.GetContext().Child.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageEditChild((sender as Button).DataContext as Child));
        }

        private void Page_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            {

                if (DtGridChild.SelectedItem == null)
                {
                    MessageBox.Show("Выберите ребенка для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                var result = MessageBox.Show("Удалить выбранного ребенка?", "Подтверждение",
                                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;

                try
                {

                    Child selectedChild = DtGridChild.SelectedItem as Child;

                    kindergartenEntities.GetContext().Child.Remove(selectedChild);
                    kindergartenEntities.GetContext().SaveChanges();

                    DtGridChild.ItemsSource = kindergartenEntities.GetContext().Child.ToList();
                    MessageBox.Show("Ребенок удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.SecondFrame.Navigate(new PageAddChild(null)); ;
        }

        private void BtnCard_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного ребенка
            var selectedChild = (sender as Button).DataContext as Child;

            if (selectedChild == null)
            {
                MessageBox.Show("Не удалось получить данные ребенка", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Загружаем медицинскую карту ребенка
                var medicalCard = kindergartenEntities.GetContext().MedicalCard
                    .FirstOrDefault(m => m.IdMedicalCard == selectedChild.idMedicalCard);

                if (medicalCard == null)
                {
                    MessageBox.Show("Медицинская карта не найдена", "Информация",
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Переходим на страницу с медицинской картой
                AppFrame.SecondFrame.Navigate(new PageMedicalCard(medicalCard));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке медицинской карты: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
