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
    /// Логика взаимодействия для DisceplinesEditWindow.xaml
    /// </summary>
    public partial class DisceplinesEditWindow : Window
    {
        private SystemLessonContext _context;
        private Disciplines _disciplines;
        private bool isAdd = false;
        public DisceplinesEditWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public bool? ShowDialog(Disciplines disciplines = null)
        {
            if (disciplines == null)
            {
                disciplines = new Disciplines();
                isAdd = true;
            }

            _disciplines = disciplines;

            DataContext = _disciplines;

            return base.ShowDialog();
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd == true)
                _context.Disciplines.Add(_disciplines);
            else
                _context.Entry(_disciplines).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
            Close();
        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {
            _context.Entry(_disciplines).State = System.Data.Entity.EntityState.Unchanged;
            Close();
        }
    }
}
