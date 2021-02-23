using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismMetroSample.Shell
{
    public static class ShellSwitcher
    {
        public static void Switch<TClose, TShow>() where TClose : Window where TShow : Window, new()
        {
            Show<TShow>();
            Close<TClose>();
        }

        public static void Show<T>(T window = null) where T : Window, new()
        {
            var shell = Application.Current.MainWindow = window ?? new T();
            shell.Show();
        }

        public static void Close<T>() where T : Window
        {
            var shell = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is T);
            shell.Close();
        }
    }
}
