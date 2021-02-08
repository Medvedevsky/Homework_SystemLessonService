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
    /// Логика взаимодействия для TeacherDisciplinesEditWindow.xaml
    /// </summary>
    public partial class TeacherDisciplinesEditWindow : Window
    {
        private SystemLessonContext _context;
        private TeacherDisciplines _teacherDisciplines;
        private bool isAdd = false;
        public TeacherDisciplinesEditWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public bool? ShowDialog(TeacherDisciplines teacherDisciplines = null)
        {
            if(teacherDisciplines == null)
            {
                teacherDisciplines = new TeacherDisciplines();
                isAdd = true;
            }

            disciplinesBox.ItemsSource = _context.Disciplines.ToList();
            groupsBox.ItemsSource = _context.Groups.ToList();
            teachersBox.ItemsSource = _context.Teachers.ToList();

            _teacherDisciplines = teacherDisciplines;
            DataContext = _teacherDisciplines;

            return base.ShowDialog();
        }
        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd == true)
                _context.TeacherDisciplines.Add(_teacherDisciplines);
            else
                _context.Entry(_teacherDisciplines).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();

            Close();
        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {
            _context.Entry(_teacherDisciplines).State = System.Data.Entity.EntityState.Unchanged;
            Close();
        }
    }
}
