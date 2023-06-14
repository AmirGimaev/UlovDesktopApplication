using System.Windows;
using System.Windows.Controls;

namespace UlovDesktopApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditProduct.xaml
    /// </summary>
    public partial class AddAndEditProduct : Page
    {
        Products _products;

        public AddAndEditProduct(Products products)
        {
            InitializeComponent();

            _products = products;

            DataContext = products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                UlovDataBase.GetContext().SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении. Пожалуйста, проверьте данные на корректоность.");
            }
        }
    }
}
