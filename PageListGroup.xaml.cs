using kindergarten.ApplicationDate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Threading;

namespace kindergarten.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageListGroup.xaml
    /// </summary>
    public partial class PageListGroup : Page
    {
        private DispatcherTimer timer;
        private Group _selectedGroup;
        private DateTime _currentDate;
        private List<ChildVisitViewModel> _childrenVisits;
        public PageListGroup(Group selectedGroup)
        {
            InitializeComponent();
  
            _selectedGroup = selectedGroup;
            DataContext = selectedGroup;

            // Устанавливаем текущую дату
            _currentDate = DateTime.Today;
            currentDateText.Text = _currentDate.ToString("dd.MM.yyyy");

            LoadChildrenVisits();

            // Настройка таймера для отображения времени
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            UpdateTime();
        }

        private void LoadChildrenVisits()
        {
            var todayVisits = kindergartenEntities.GetContext().Visit
                .Where(v => DbFunctions.TruncateTime(v.Date) == _currentDate.Date)
                .ToList();

            var children = kindergartenEntities.GetContext().Child
                .Where(x => x.idGroup == _selectedGroup.idGroup)
                .ToList();

            _childrenVisits = new List<ChildVisitViewModel>();

            foreach (var child in children)
            {
                var visit = todayVisits.FirstOrDefault(v => v.idChild == child.idСhild);

                _childrenVisits.Add(new ChildVisitViewModel
                {
                    ChildId = child.idСhild,
                    Surname = child.Surname,
                    NameChild = child.NameChild,
                    IsPresent = visit == null ? true : visit.Status == "Присутствует",
                    AbsenceReason = visit?.ReasoneOfAbsence
                });
            }

            DtGridGroupList.ItemsSource = _childrenVisits;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();

            // Проверяем, изменилась ли дата
            if (DateTime.Today != _currentDate)
            {
                _currentDate = DateTime.Today;
                currentDateText.Text = _currentDate.ToString("dd.MM.yyyy");
                LoadChildrenVisits();
            }
        }

        private void UpdateTime()
        {
            timeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = kindergartenEntities.GetContext();

                foreach (var childVisit in _childrenVisits)
                {
                    var existingVisit = context.Visit.FirstOrDefault(v =>
                        v.idChild == childVisit.ChildId &&
                        DbFunctions.TruncateTime(v.Date) == _currentDate.Date);

                    if (existingVisit != null)
                    {
                        // Обновляем существующую запись
                        existingVisit.Status = childVisit.IsPresent ? "Присутствует" : "Отсутствует";
                        existingVisit.ReasoneOfAbsence = childVisit.IsPresent ? null : childVisit.AbsenceReason;
                    }
                    else
                    {
                        // Создаем новую запись
                        var newVisit = new Visit
                        {
                            idChild = childVisit.ChildId,
                            Date = _currentDate,
                            Status = childVisit.IsPresent ? "Присутствует" : "Отсутствует",
                            ReasoneOfAbsence = childVisit.IsPresent ? null : childVisit.AbsenceReason
                        };
                        context.Visit.Add(newVisit);
                    }
                }

                context.SaveChanges();
                MessageBox.Show("Данные посещаемости сохранены успешно!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ChildVisitViewModel
    {
        public int ChildId { get; set; }
        public string Surname { get; set; }
        public string NameChild { get; set; }
        public bool IsPresent { get; set; }
        public string AbsenceReason { get; set; }
    }
}
