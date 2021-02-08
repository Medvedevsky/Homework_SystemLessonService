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
    /// Логика взаимодействия для GroupListWindow.xaml
    /// </summary>
    public partial class GroupListWindow : Window
    {
        private SystemLessonContext _context;
        public GroupListWindow(SystemLessonContext context)
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
            groupsList.ItemsSource = _context.Groups.ToList();
        }
      
        private void addGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var editWindow = new GroupEditWindow(_context);
            editWindow.ShowDialog();
            ShowDialog();
        }

        private void deleteGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedGroups = groupsList.SelectedItem as Groups;

            if (selectedGroups == null)
            {
                MessageBox.Show("Выделите группу для удаления!");
                return;
            }
            _context.Groups.Remove(selectedGroups);
            _context.SaveChanges();
        }

       

        private void groupsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            var editWindow = new GroupEditWindow(_context);
            var selectedGroups = groupsList.SelectedItem as Groups;
            editWindow.ShowDialog(selectedGroups);
            ShowDialog();
        }
    }
}
