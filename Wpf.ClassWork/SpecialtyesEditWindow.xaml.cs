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
    /// Логика взаимодействия для SpecialtyesEditWindow.xaml
    /// </summary>
    public partial class SpecialtyesEditWindow : Window
    {
        private SystemLessonContext _context;
        private bool isAdd = false;
        private Specialtys _specialtys;
        public SpecialtyesEditWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public bool? ShowDialog(Specialtys specialtys = null)
        { 
            if(specialtys == null)
            {
                specialtys = new Specialtys();
                isAdd = true;
            }
            _specialtys = specialtys;

            DataContext = _specialtys;

            return base.ShowDialog();
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd == true)
                _context.Specialtys.Add(_specialtys);
            else
                _context.Entry(_specialtys).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();

            Close();
        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {
            _context.Entry(_specialtys).State = System.Data.Entity.EntityState.Unchanged;
            Close();
        }
    }
}
