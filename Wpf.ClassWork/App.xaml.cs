using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SystemLessonStorage;

namespace Wpf.ClassWork
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            App app = new App();
            var globalContext = new SystemLessonContext();
            var mainWindow = new MainWindow(globalContext);
            app.Run(mainWindow);
        }
    }
}
