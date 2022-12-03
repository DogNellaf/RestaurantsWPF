using RestaurantsClasses.OnlineSystem;

namespace RestaurantsClasses.BookingSystem
{
    // бронь
    public class Booking: Model
    {
        // дата и время, на которое забронировано
        public DateTime Time { get; }

        // фамилия клиента
        public Client Client { get; }

        // оплачена ли бронь
        public bool IsPaid { get; }

        // стол
        public Table Table { get; }

        // конструктор
        public Booking(int id, DateTime time, Client client, bool isPaid, Table table): base(id)
        {
            if (client is null)
            {
                throw new Exception("Клиент обязательно должен быть задан");
            }
            Time = time;
            Client = client;
            IsPaid = isPaid;
            Table = table;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Бронь на {Time} от клиента {Client.FirstName} {Client.SecondName} на столик {Table.Id}";
        }
    }
}
