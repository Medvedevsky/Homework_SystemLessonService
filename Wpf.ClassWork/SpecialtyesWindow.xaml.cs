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
    /// Логика взаимодействия для SpecialtyesWindow.xaml
    /// </summary>
    public partial class SpecialtyesWindow : Window
    {
        private SystemLessonContext _context;
        public SpecialtyesWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                Reload();
        }

        private void Reload()
        {
            specialtyesList.ItemsSource = _context.Specialtys.ToList();
        }

        private void specialtyesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            
            var selectSpec = specialtyesList.SelectedItem as Specialtys;
            if (selectSpec != null)
            {
                var editWindow = new SpecialtyesEditWindow(_context);
                editWindow.ShowDialog(selectSpec);
            }
            ShowDialog();
        }

        private void addSpecBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var spec = new Specialtys();
            var editWindow = new SpecialtyesEditWindow(_context);
            editWindow.ShowDialog();
            ShowDialog();
        }

        private void deleteSpecBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpec = specialtyesList.SelectedItem as Specialtys;

            if (selectedSpec != null)
                _context.Specialtys.Remove(selectedSpec);

            _context.SaveChanges();
            Reload();
        }
    }
}
