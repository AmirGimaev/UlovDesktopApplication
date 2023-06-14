using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UlovDesktopApplication.Classes;

namespace UlovDesktopApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для MakeOrderWindow.xaml
    /// </summary>
    public partial class MakeOrderWindow : Window
    {
        public MakeOrderWindow()
        {
            InitializeComponent();

            PointsOfIsusseComboBox.ItemsSource = UlovDataBase.GetContext().PointsOfIsusse.ToList();

            SelectedOrdersDataGrid.ItemsSource = StaticClass.Cart;

            CalculatePrice();
        }

        private void CalculatePrice()
        {
            var _priceWithoutDiscount = StaticClass.Cart.Sum(s => s.Count * s.Products.Price);

            var _priceWithDiscount = StaticClass.Cart.Sum(s => s.Products.Price - (s.Products.Price * (s.Products.DiscountAmount / 100)) * s.Count);

            TotalPriceWithoutDiscountTextBlock.Text = $"Сумма без учета скидки - {String.Format("{0:N2}", _priceWithoutDiscount)} руб.";

            TotalPriceWithDiscountTextBlock.Text = $"Сумма с учета скидки - {String.Format("{0:N2}", _priceWithDiscount)} руб.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StaticClass.Cart.Count == 0)
            {
                MessageBox.Show("Список пуст.");
                return;
            }

            if (PointsOfIsusseComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пункт выдачи.");
                return;
            }

            try
            {
                var number_coupon = new Random().Next(0, 999).ToString("000");

                foreach (var _order in StaticClass.Cart)
                {
                    if (_order.Count != 0)
                    {
                        UlovDataBase.GetContext().Orders.Add(_order);

                        UlovDataBase.GetContext().SaveChanges();

                        CompositonOrders _compositonOrder = new CompositonOrders()
                        {
                            IDOrder = _order.ID,
                            IDPointOfIsusse = (PointsOfIsusseComboBox.SelectedItem as PointsOfIsusse).ID,
                            PointsOfIsusse = PointsOfIsusseComboBox.SelectedItem as PointsOfIsusse,
                            IDStatus = UlovDataBase.GetContext().Statuses.Where(w => w.Status == "Новый").First().ID,
                            Date = DateTime.Today,
                            Coupon = number_coupon
                        };

                        UlovDataBase.GetContext().CompositonOrders.Add(_compositonOrder);

                        UlovDataBase.GetContext().SaveChanges();
                    }
                }

                MessageBox.Show("Заказ оформлен. Ваш талон - " + number_coupon);

                StaticClass.Cart.Clear();

                Pages.ProductListPage.This.MakeOrderButton.Visibility = Visibility.Hidden;

                Close();
            }
            catch
            {
                MessageBox.Show("Произошла непредвиденная ошибка. Пожалуйста, проверьте данные на корректность и повторите операцию.");
            }
        }

        private void SelectedOrdersDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CalculatePrice();
            }
            catch
            {

            }
        }

        private void SelectedOrdersDataGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                CalculatePrice();
            }
            catch
            {

            }
        }
    }
}
