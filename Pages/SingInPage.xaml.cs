using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UlovDesktopApplication.Classes;

namespace UlovDesktopApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для SingInPage.xaml
    /// </summary>
    public partial class SingInPage : Page
    {
        public SingInPage()
        {
            InitializeComponent();
        }

        private void SingIn(object sender, RoutedEventArgs e)
        {
            var user = UlovDataBase.GetContext().Users.Where(w => w.Login == LoginTextBox.Text & w.Password == PasswordTextBox.Password).FirstOrDefault();

            if (user != null)
            {
                var _client = UlovDataBase.GetContext().Clients.Where(w => w.IDUser == user.ID).FirstOrDefault();
                var _emplyee = UlovDataBase.GetContext().Employees.Where(w => w.IDUser == user.ID).FirstOrDefault();

                if (_client != null)
                {
                    StaticClass.User = _client;
                }
                else
                {
                    StaticClass.User = _emplyee; 
                }

                NavigationService.Content = new ProductListPage();

            }
            else
            {
                MessageBox.Show("Пользователь не найден.");
            }
        }

        private void SingInAsGuest(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new ProductListPage();
        }
    }
}
