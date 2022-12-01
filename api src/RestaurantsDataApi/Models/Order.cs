using RestaurantsDataApi.Enums;

namespace RestaurantsDataApi.Models
{
    // заказ
    public class Order
    {
        // id из базы
        public int Id { get; }

        // блюда в заказе
        public List<Meal_to_Order> Meals { get; }

        // сумма заказа
        public double Summa => Meals.Sum(x => x.Meal.Cost * x.Count);

        // статус заказа
        public OrderStatus Status { get; private set; }

        // дата
        public DateTime Created { get; }

        // код стола
        public int TableCode { get; }

        // официант
        public Worker Server { get; }

        // конструктор
        public Order(int id, OrderStatus status, DateTime created, int tableCode, Worker server)
        {
            Id = id;
            Status = status;
            Created = created;
            TableCode = tableCode;
            Server = server;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Заказ {Id} за столиком {TableCode} в данный момент {Status}. " +
                $"Дата создания - {Created}. Обслуживает {Server.FirstName} {Server.LastName}.";
        }
    }
}
