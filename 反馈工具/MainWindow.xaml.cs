using System.Windows;

namespace 反馈工具
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Send(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("反馈信息发送成功", "反馈工具", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
