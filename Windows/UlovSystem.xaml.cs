using System.Windows;

namespace UlovDesktopApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для UlovSystem.xaml
    /// </summary>
    public partial class UlovSystem : Window
    {
        public UlovSystem()
        {
            InitializeComponent();

            MainFrame.Content = new Pages.SingInPage();
        }
    }
}
