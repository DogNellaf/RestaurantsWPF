using RestaurantsDataApi.Enums;

namespace RestaurantsDataApi.Models
{
    // заказ
    public class Order
    {
        // id из базы
        public int Id { get; }

        // блюда в заказе
        // TODO

        // сумма заказа
        // TODO

        // статус заказа
        public OrderStatus Status { get; private set; }

        // дата
        public DateTime Created { get; }

        // код стола
        public int TableCode { get; }

        // официант
        // TODO
    }
}
