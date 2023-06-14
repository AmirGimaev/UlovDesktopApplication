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
                if (_products.ID == 0) UlovDataBase.GetContext().Products.Add(_products);

                UlovDataBase.GetContext().SaveChanges();

                MessageBox.Show("Запись сохранена.");

                NavigationService.Content = new ProductListPage();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении. Пожалуйста, проверьте данные на корректоность и повторите попытку.");
            }
        }
    }
}
