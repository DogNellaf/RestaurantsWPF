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
        public int ServerId = -1;

        // id столика
        public int TableId = -1;

        // блюда в заказе
        //public Dictionary<Meal, int> Meals { get; }

        // сумма заказа
        //public double Summa => Meals.Sum(x => x.Key.Cost * x.Value);

        // статус заказа
        public OrderStatus Status { get; set; }

        // дата
        public DateTime Created { get; set; }

        // стол
        //public Table Table => Database.GetObject<Table>($"id = {_table_id}").FirstOrDefault();

        // официант
        //public Worker Server => Database.GetObject<Worker>($"id = {_table_id}").FirstOrDefault();

        // конструктор
        public OfflineOrder(int id, int status_id, DateTime created, int table_id, int server_id) : base(id)
        {
            Status = (OrderStatus)status_id;
            Created = created;
            TableId = table_id;
            ServerId = server_id;
        }

        public OfflineOrder(object[] items) : base((int)items[0])
        {
            Status = (OrderStatus)((int)items[1]);
            Created = (DateTime)items[2];

            if (!string.IsNullOrEmpty(items[3].ToString()))
            {
                TableId = (int)items[3];
            }

            if (!string.IsNullOrEmpty(items[4].ToString()))
            {
                ServerId = (int)items[4];
            }
        }

        public OfflineOrder()
        {

        }

        // текстовый вывод
        //public override string ToString()
        //{
        //    return $"Заказ {id} за столиком {Table.id} в данный момент {Status}. " +
        //        $"Дата создания - {Created}. Обслуживает {Server.FirstName} {Server.LastName}.";
        //}
    }
}
