using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для StudentEditorWindow.xaml
    /// </summary>
    public partial class StudentEditorWindow : Window
    {
        private SystemLessonContext _context;
        private bool isActive = false;
        private Students _students;
        private List<Groups> groups;
        private string oldFileName;
        public StudentEditorWindow(SystemLessonContext context)
        {
            InitializeComponent();
            _context = context;
            groups = _context.Groups.ToList();

        }

        private void UpdateStudentPhoto(string newPhotoFileName)
        {
            photoBox.Text = newPhotoFileName;
            var binding = photoBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
        private void uploadPictureBtn_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Pictures |*.png; *.jpg; *.bmp";
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = false;

            if (fileDialog.ShowDialog() == true)
                UpdateStudentPhoto(fileDialog.FileName);

            oldFileName = fileDialog.FileName;
        }

        private string CloneFileToImage()
        {
            var newFileName = DateTime.Now.Ticks.ToString() + System.IO.Path.GetFileName(_students.Photo);

            var fullNewFileName = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", newFileName);

            File.Copy(_students.Photo, fullNewFileName);

            return newFileName;
        }

        private void DeleteFromImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var fullFileName = DateTime.Now.Ticks.ToString() + System.IO.Path.Combine(Environment.CurrentDirectory,
                "Resources",
                fileName);

            if (File.Exists(fullFileName))
                File.Delete(fullFileName);
        }

        public bool? ShowDialog(Students students = null)
        {
            if (students == null)
            {
                students = new Students();
                isActive = true;
            }

            _students = students;
            oldFileName = _students.Photo;


            studentsBox.ItemsSource = _context.Groups.ToList();
            DataContext = _students;

            return base.ShowDialog();
        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_students.Photo))
            {
                DeleteFromImage(oldFileName);
                var fileName = CloneFileToImage();
                UpdateStudentPhoto(fileName);
            }


            if (isActive == true)
                _context.Students.Add(_students);
            else
                _context.Entry(_students).State = EntityState.Modified;


            _context.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
         
            UpdateStudentPhoto(oldFileName);
            _context.Entry(_students).State = EntityState.Unchanged;
            DialogResult = false;
            Close();
        }
    }
}
