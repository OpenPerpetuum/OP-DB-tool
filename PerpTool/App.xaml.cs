using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PerpTool
{


    public static class Global
    {
        public static string ConnectionString { get; set; } = @"Server=localhost\PERPSQL;Database=perpetuumsa;Trusted_Connection=True;Pooling=True;Connection Timeout=30;Connection Lifetime=260;Connection Reset=True;Min Pool Size=20;Max Pool Size=60;";
    }


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // open a logindialog.            
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var mainWindow = new MainWindow();
            Current.MainWindow = mainWindow;
            mainWindow.Show();

            //var dialog = new Dialogs.LoginDialog();
            //if (dialog.ShowDialog() == true)
            //{
            //    var mainWindow = new MainWindow();
            //    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //    Current.MainWindow = mainWindow;
            //    mainWindow.Show();
            //}
        }

        private void DragablzItemsControl_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
