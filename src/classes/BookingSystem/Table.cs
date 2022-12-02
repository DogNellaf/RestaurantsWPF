namespace RestaurantsClasses.BookingSystem
{
    // столик в ресторане
    public class Table
    {
        // id стола из базы
        public int Id { get; }

        // количество мест
        public int SeatsCount { get; }

        // занят ли в текущий момент
        public bool IsReserved { get; }

        // конструктор
        public Table(int id, int seatsCount, bool isReserved)
        {
            Id = id;
            SeatsCount = seatsCount;
            IsReserved = isReserved;
        }
    }
}
