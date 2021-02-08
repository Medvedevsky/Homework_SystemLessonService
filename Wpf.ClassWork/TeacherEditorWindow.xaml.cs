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
using System.Data.Entity;
using Microsoft.Win32;
using System.IO;

namespace Wpf.ClassWork
{
    /// <summary>
    /// Логика взаимодействия для TeacherEditorWindow.xaml
    /// </summary>
    public partial class TeacherEditorWindow : Window
    {
        private SystemLessonContext _context;
        private Teachers _teacher;
        private bool isCreation = false;
        private string oldFileName;

        public TeacherEditorWindow(SystemLessonContext context, Teachers teacher = null)
        {
            InitializeComponent();
            _context = context;
            _teacher = teacher;

            if (teacher == null)
            {
                teacher = new Teachers();
                isCreation = true;
            }
            _teacher = teacher;
            oldFileName =_teacher.Photo;
        }


        private void uploadPictureBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = "Pictures |*.png; *.jpg; *.bmp";
            openFile.RestoreDirectory = true;
            openFile.Multiselect = false;


            if (openFile.ShowDialog() == true)
                UpdateStudentPhoto(openFile.FileName);

        }
        private void UpdateStudentPhoto(string newPhotoFileName)
        {
            photoBox.Text = newPhotoFileName;
            var binding = photoBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private string CloneFileToPictures()
        {
            var newFileName = DateTime.Now.Ticks.ToString()
                  + System.IO.Path.GetFileName(_teacher.Photo);


            var fullNewFileName = System.IO.Path.Combine(Environment.CurrentDirectory,
                "Resources",
                newFileName);

            File.Copy(_teacher.Photo, fullNewFileName);
            return newFileName;
        }

        private void DeleteFromPicture(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var fullFileName = System.IO.Path.Combine(
                  Environment.CurrentDirectory,
                  "Resources",
                  fileName
              );

            if (File.Exists(fullFileName))
                File.Delete(fullFileName);

        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(_teacher.Photo))
            {
                DeleteFromPicture(oldFileName);
                var fileName = CloneFileToPictures();
                UpdateStudentPhoto(fileName);
            }

            if (isCreation)
            {
                _context.Teachers.Add(_teacher);
            }
            else
                _context.Entry(_teacher).State = EntityState.Modified;


            _context.SaveChanges();
            Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateStudentPhoto(oldFileName);
            _context.Entry(_teacher).State = EntityState.Unchanged;
            Close();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                DataContext = _teacher;
        }
    }
}
