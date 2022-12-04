namespace RestaurantsClasses.BookingSystem
{
    // столик в ресторане
    public class Table: Model
    {

        // количество мест
        public int SeatsCount { get; }

        // занят ли в текущий момент
        public bool IsReserved { get; }

        // конструктор
        public Table(int id, int seatsCount, bool isReserved): base(id)
        {
            SeatsCount = seatsCount;
            IsReserved = isReserved;
        }

        public Table(object[] items) : base((int)items[0])
        {
            IsReserved = (bool)items[1];
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Столик {id}, количество мест {SeatsCount}";
        }
    }
}
