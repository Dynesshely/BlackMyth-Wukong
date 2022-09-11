using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 黑神话_悟空
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Thread.Sleep(App.Random.Next(App.Random.Next(1500, 2500), App.Random.Next(4500, 7500)));
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                }));

                Thread.Sleep(App.Random.Next(App.Random.Next(1000, 2000), App.Random.Next(3000, 4000)));
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Begin_EntranceAnimation();
                }));
            }).Start();
        }

        private void Begin_EntranceAnimation()
        {

        }
    }
}
