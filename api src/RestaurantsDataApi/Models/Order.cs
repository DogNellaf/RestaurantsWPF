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

        // стол
        public Table Table { get; }

        // официант
        public Worker Server { get; }

        // конструктор
        public Order(int id, OrderStatus status, DateTime created, Table table, Worker server)
        {
            if (server is null)
            {
                throw new Exception("Каждый заказ должен быть обслужен официантом");
            }

            Id = id;
            Status = status;
            Created = created;
            Table = table;
            Server = server;
            Meals = new List<Meal_to_Order>();
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Заказ {Id} за столиком {Table.Id} в данный момент {Status}. " +
                $"Дата создания - {Created}. Обслуживает {Server.FirstName} {Server.LastName}.";
        }
    }
}
