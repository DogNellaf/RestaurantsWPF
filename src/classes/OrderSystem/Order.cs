using RestaurantsClasses;
using RestaurantsClasses.BookingSystem;
using RestaurantsClasses.Enums;
using RestaurantsClasses.WorkersSystem;

namespace RestaurantsClasees.OrderSystem
{
    // заказ
    public class OfflineOrder: Model
    {
        // id официанта
        private int _server_id;

        // id столика
        private int _table_id;

        // блюда в заказе
        public Dictionary<Meal, int> Meals { get; }

        // сумма заказа
        public double Summa => Meals.Sum(x => x.Key.Cost * x.Value);

        // статус заказа
        public OrderStatus Status { get; private set; }

        // дата
        public DateTime Created { get; }

        // стол
        public Table Table => Database.GetObject<Table>($"id = {_table_id}").FirstOrDefault();

        // официант
        public Worker Server => Database.GetObject<Worker>($"id = {_table_id}").FirstOrDefault();

        // конструктор
        public OfflineOrder(int id, int status_id, DateTime created, int table_id, int server_id) : base(id)
        {
            Status = (OrderStatus)status_id;
            Created = created;
            _table_id = table_id;
            _server_id = server_id;
        }

        public OfflineOrder(object[] items) : base((int)items[0])
        {
            Status = (OrderStatus)((int)items[1]);
            Created = (DateTime)items[1];
            _table_id = (int)items[2];
            _server_id = (int)items[3];
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Заказ {Id} за столиком {Table.Id} в данный момент {Status}. " +
                $"Дата создания - {Created}. Обслуживает {Server.FirstName} {Server.LastName}.";
        }
    }
}
