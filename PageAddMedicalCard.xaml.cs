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
   
    public partial class PageAddMedicalCard : Page
    {
        private MedicalCard _currentMedicalCard = new MedicalCard();
        public PageAddMedicalCard()
        {
            InitializeComponent();
            DataContext = _currentMedicalCard;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentMedicalCard.DateCreation == null)
                errors.AppendLine("Укажите дату");
            if (_currentMedicalCard.GeneralInformation == null)
                errors.AppendLine("Укажите информацию");
            if (_currentMedicalCard.IdVaccination <= 0)
                errors.AppendLine("Укажите индекс прививки");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentMedicalCard.IdMedicalCard == 0)
                kindergartenEntities.GetContext().MedicalCard.Add(_currentMedicalCard);

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
            AppFrame.FrameMain.Navigate(new PageMedicalCard());
        }
    }
}
