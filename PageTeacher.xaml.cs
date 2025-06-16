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
    
    public partial class PageTeacher : Page
    {
        public PageTeacher()
        {
            InitializeComponent();
            DtGridTeacher.ItemsSource = kindergartenEntities.GetContext().Teacher.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            
            if (DtGridTeacher.SelectedItem == null)
            {
                MessageBox.Show("Выберите учителя для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Удалить выбранного воспитателя?", "Подтверждение",
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                Teacher selectedTeacher = DtGridTeacher.SelectedItem as Teacher;

                
                kindergartenEntities.GetContext().Teacher.Remove(selectedTeacher);
                kindergartenEntities.GetContext().SaveChanges();

                
                DtGridTeacher.ItemsSource = kindergartenEntities.GetContext().Teacher.ToList();
                MessageBox.Show("Воспитатель удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageAddTeacher());
        }
    }

       
    

}
