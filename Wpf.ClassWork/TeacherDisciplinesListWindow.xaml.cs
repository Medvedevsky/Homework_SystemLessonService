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
    /// Логика взаимодействия для TeacherDisciplinesListWindow.xaml
    /// </summary>
    public partial class TeacherDisciplinesListWindow : Window
    {
        private SystemLessonContext _context;
        public TeacherDisciplinesListWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void TeacherDisciplinesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            var selectItem = teacherDisciplinesList.SelectedItem as TeacherDisciplines;

            if(selectItem != null)
            {
                var editWindow = new TeacherDisciplinesEditWindow(_context);
                editWindow.ShowDialog(selectItem);
            }
            ShowDialog();
        }

        private void Reload()
        {
            teacherDisciplinesList.ItemsSource = _context.TeacherDisciplines.ToList();
        }

        private void addTDBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var editWindow = new TeacherDisciplinesEditWindow(_context);
            editWindow.ShowDialog();
            ShowDialog();
        }

        private void deleteTDBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectItems = teacherDisciplinesList.SelectedItem as TeacherDisciplines;

            if(selectItems != null)
            {
                _context.TeacherDisciplines.Remove(selectItems);
                _context.SaveChanges();
                Reload();
            }

        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                Reload();
        }
    }
}
