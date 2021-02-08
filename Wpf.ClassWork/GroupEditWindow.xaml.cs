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
    /// Логика взаимодействия для GroupEditWindow.xaml
    /// </summary>
    public partial class GroupEditWindow : Window
    {
        private SystemLessonContext _context;
        private Groups _groups;
        private bool isAdd = false;
        public GroupEditWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }
        
        public bool? ShowDialog(Groups groups= null)
        {
            if (groups == null)
            {
                groups = new Groups();
                isAdd = true;
            }    

            _groups = groups;

            groupBox.ItemsSource = _context.Specialtys.ToList();
            DataContext = _groups;

           

            return base.ShowDialog();
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd==true)
                _context.Groups.Add(_groups);
            else
                _context.Entry(_groups).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();

            Close();
        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
