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
using System.Windows.Shapes;
using SystemLessonStorage;

namespace Wpf.ClassWork
{
    /// <summary>
    /// Логика взаимодействия для DisciplinesListWindow.xaml
    /// </summary>
    public partial class DisciplinesListWindow : Window
    {
        private SystemLessonContext _context;
        public DisciplinesListWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
            
        }

        private void addDisBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var windowEdit = new DisceplinesEditWindow(_context);
            windowEdit.ShowDialog();
            ShowDialog();
        }

        private void deleteDisBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedDis = disciplinesList.SelectedItem as Disciplines;

            if (selectedDis != null)
            {
                _context.Disciplines.Remove(selectedDis);
                _context.SaveChanges();
                Reload();
            }

          
        }

        private void Reload()
        {
            disciplinesList.ItemsSource = _context.Disciplines.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                Reload();
        }

        private void disciplinesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            var selectDis = disciplinesList.SelectedItem as Disciplines;
            var window = new DisceplinesEditWindow(_context);
            window.ShowDialog(selectDis);
            ShowDialog();
        }
    }
}
