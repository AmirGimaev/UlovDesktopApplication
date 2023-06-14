using System.Collections.Generic;


namespace UlovDesktopApplication.Classes
{
    public static class StaticClass
    {
        public static object User { get; set; } // хранит данные об пользователе (клиента или сотрудника)

        public static List<Orders> Cart = new List<Orders>(); // корзина заказа
    }
}
