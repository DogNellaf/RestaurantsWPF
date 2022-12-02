﻿namespace RestaurantsClasses.BookingSystem
{
    // бронь
    public class Booking: Model
    {

        // дата и время, на которое забронировано
        public DateTime Time { get; }

        // фамилия клиента
        public string ClientLastName { get; }

        // оплачена ли бронь
        public bool IsPaid { get; }

        // стол
        public Table Table { get; }

        // конструктор
        public Booking(int id, DateTime time, string clientLastName, bool isPaid, Table table): base(id)
        {
            Time = time;
            ClientLastName = clientLastName;
            IsPaid = isPaid;
            Table = table;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Бронь на {Time} от клиента {ClientLastName} на столик {Table.Id}";
        }
    }
}
