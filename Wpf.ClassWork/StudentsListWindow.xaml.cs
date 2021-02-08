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
    /// Логика взаимодействия для StudentsListWindow.xaml
    /// </summary>
    public partial class StudentsListWindow : Window
    {
        private SystemLessonContext _context;
        public StudentsListWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
            
        }
        private void Reload()
        {
            studentsList.ItemsSource = _context.Students.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                Reload();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var window = new StudentEditorWindow(_context);
            window.ShowDialog();
            ShowDialog();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectStudent = studentsList.SelectedItem as Students;
            if(selectStudent !=null)
            {
                _context.Students.Remove(selectStudent);
                _context.SaveChanges();
                Reload();
            }
        }

        private void studentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            
            var selectStudent = studentsList.SelectedItem as Students;
            var windowEditor = new StudentEditorWindow(_context);
            windowEditor.ShowDialog(selectStudent);
            ShowDialog();
        }
    }
}
