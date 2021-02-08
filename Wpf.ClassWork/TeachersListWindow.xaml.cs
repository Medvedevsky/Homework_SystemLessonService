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
    /// Логика взаимодействия для TeachersListWindow.xaml
    /// </summary>
    public partial class TeachersListWindow : Window
    {
        private SystemLessonContext _context;
        public TeachersListWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }
        private void ReloadData()
        {
            teachersList.ItemsSource = _context.Teachers.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                ReloadData();
        }

        private void addBtnTeacher_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            var widnwoEditor = new TeacherEditorWindow(_context);
            widnwoEditor.ShowDialog();
            ShowDialog();
            
        }

        private void deleteBtnTeacher_Click(object sender, RoutedEventArgs e)
        {
            var selectTeacher = teachersList.SelectedItem as Teachers;
            if (selectTeacher != null)
            {
                _context.Teachers.Remove(selectTeacher);
                _context.SaveChanges();
                ReloadData();
            }
            
        }

       

        private void teachersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            var selectTeacher = teachersList.SelectedItem as Teachers;
            var windowEditor = new TeacherEditorWindow(_context, selectTeacher);
            windowEditor.ShowDialog();
            ShowDialog();
        }
    }
}
