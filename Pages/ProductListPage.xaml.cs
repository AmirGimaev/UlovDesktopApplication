using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UlovDesktopApplication.Classes;

namespace UlovDesktopApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public ProductListPage()
        {
            InitializeComponent();

            This = this;

            if (StaticClass.User is Employees)
            {
                var _employee = StaticClass.User as Employees;

                if (_employee.Postes.Name == "Менеджер")
                {
                    ShowOrderListPageButton.Visibility = Visibility.Visible;
                }
                else if (_employee.Postes.Name == "Администратор")
                {
                    ShowOrderListPageButton.Visibility = Visibility.Visible;

                    AddProductButton.Visibility = Visibility.Visible;
                    EditProductButton.Visibility = Visibility.Visible;
                    DeleteProductButton.Visibility = Visibility.Visible;
                }
            }

            ShowPage(ref current_page);
        }

        public static ProductListPage This;

        List<Products> _products = new List<Products>();

        int count_page = 5, // количество товара на одном странице 
            current_page = 0; // текущая страница

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            current_page--;
            ShowPage(ref current_page);
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            current_page++;
            ShowPage(ref current_page);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            StaticClass.User = null;

            NavigationService.Content = new SingInPage();
        }

        private void ShowMakeOrderWindow(object sender, RoutedEventArgs e)
        {
            new Windows.MakeOrderWindow().ShowDialog();
        }

        private void AddOrderToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                var _product = ProductListView.SelectedItem as Products;

                Orders _order = new Orders()
                {
                    IDProduct = _product.ID,
                    Products = _product,
                    Count = 1 // количество товара
                };

                StaticClass.Cart.Add(_order);

                MakeOrderButton.Visibility = Visibility.Visible;

                MessageBox.Show("Товар добавлен в корзину заказов.");
            }
            catch
            {

            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var _selectedProducts = ProductListView.SelectedItems as List<Products>;

            if(_selectedProducts is null)
            {
                MessageBox.Show("Выберите товары для удаления");
                return;
            }

            var result = MessageBox.Show("Вы точно хотите удалить данные записи?", "Уведомление", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                UlovDataBase.GetContext().Products.RemoveRange(_selectedProducts);

                UlovDataBase.GetContext().SaveChanges();

                MessageBox.Show("операция прошла успешно");
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new AddAndEditProduct(new Products());
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _selectedProduct = ProductListView.SelectedItem as Products;

                NavigationService.Content = new AddAndEditProduct(_selectedProduct);
            }
            catch
            {
                MessageBox.Show("Выберите запись.");
            }
        }

        private void ShowPage(ref int number_page)
        {
            _products = UlovDataBase.GetContext().Products.ToList();

            if (number_page < 0)
            {
                number_page = 0;
                return;
            }

            if (number_page * count_page >= _products.Count)
            {
                number_page--;
                return;
            }

            ProductListView.Items.Clear(); 

            try
            {
                for (int i = number_page * count_page; i < number_page * count_page + count_page; i++)
                {
                    ProductListView.Items.Add(_products[i]);
                }
            }
            catch
            {

            }

            CurrentPageTextBlock.Text = (number_page + 1).ToString();
        }
    }
}
