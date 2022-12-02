using RestaurantsClasses.BookingSystem;
using RestaurantsClasses.Enums;
using RestaurantsClasses.WorkersSystem;

namespace RestaurantsClasees.OrderSystem
{
    // заказ
    public class OfflineOrder
    {
        // id из базы
        public int Id { get; }

        // блюда в заказе
        public Dictionary<Meal, int> Meals { get; }

        // сумма заказа
        public double Summa => Meals.Sum(x => x.Key.Cost * x.Value);

        // статус заказа
        public OrderStatus Status { get; private set; }

        // дата
        public DateTime Created { get; }

        // стол
        public Table Table { get; }

        // официант
        public Worker Server { get; }

        // конструктор
        public OfflineOrder(int id, OrderStatus status, DateTime created, Table table, Worker server)
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
            Meals = new Dictionary<Meal, int>();
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Заказ {Id} за столиком {Table.Id} в данный момент {Status}. " +
                $"Дата создания - {Created}. Обслуживает {Server.FirstName} {Server.LastName}.";
        }
    }
}
