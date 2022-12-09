using RestaurantsClasses.OnlineSystem;

namespace RestaurantsClasses.BookingSystem
{
    // бронь
    public class Booking: Model
    {
        // id клиента
        public int ClientId;

        // id столика
        public int TableId;

        // дата и время, на которое забронировано
        public DateTime Time { get; set; }

        // клиент
        //public Client Client => Database.GetObject<Client>($"where id = {_client_id}").FirstOrDefault();

        // оплачена ли бронь
        public bool IsPaid { get; set; }

        // стол
        //public Table Table => Database.GetObject<Table>($"where id = {_table_id}").FirstOrDefault();

        // конструктор
        public Booking(int id, DateTime time, int client_id, bool isPaid, int table_id): base(id)
        {
            Time = time;
            ClientId = client_id;
            TableId = table_id;
            IsPaid = isPaid;
        }

        public Booking(object[] items) : base((int)items[0])
        {
            Time = (DateTime)items[1];
            ClientId = (int)items[2];
            IsPaid = (bool)items[3];
            TableId = (int)items[4];
        }

        // текстовый вывод
        //public override string ToString()
        //{
        //    return $"Бронь на {Time} от клиента {Client.FirstName} {Client.SecondName} на столик {Table.id}";
        //}
    }
}
