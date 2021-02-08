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
using SystemLessonStorage;

namespace Wpf.ClassWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SystemLessonContext _context;
        public MainWindow(SystemLessonContext globalContext)
        {
            InitializeComponent();
            _context = globalContext;
        }

        private void toTeachersList_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var teachersWindow = new TeachersListWindow(_context);
            teachersWindow.ShowDialog();
            ShowDialog();
        }

        private void toStudentsListBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var studentsWindow = new StudentsListWindow(_context);
            studentsWindow.ShowDialog();
            ShowDialog();
        }

        private void toDisciplinesBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var disciplineWindow = new DisciplinesListWindow(_context);
            disciplineWindow.ShowDialog();
            ShowDialog();
        }

        private void toGroupsListBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var groupListWindow = new GroupListWindow(_context);
            groupListWindow.ShowDialog();
            ShowDialog();
        }

        private void toSpecialtyesBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var specWindow = new SpecialtyesWindow(_context);
            specWindow.ShowDialog();
            ShowDialog();
        }

        private void toTeacherDisciplinesBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var teacherDisciplinesWindow = new TeacherDisciplinesListWindow(_context);
            teacherDisciplinesWindow.ShowDialog();
            ShowDialog();
        }
    }
}
