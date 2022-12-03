using RestaurantsClasses.Enums;

namespace RestaurantsClasses.WorkersSystem
{
    // сотрудник
    public class Worker: Model
    {
        // id должности
        private int _position_id;

        // имя
        public string FirstName { get; }

        // фамилия 
        public string LastName { get; }

        // телефон 
        public string? Phone { get; }

        // должность сотрудника
        public Position Position => Database.GetObject<Position>($"id = {_position_id}").FirstOrDefault();

        // конструктор
        public Worker(int id, string firstName, string lastName, string? phone, int position_id) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            _position_id = position_id;
        }

        public Worker(object[] items) : base((int)items[0])
        {
            FirstName = items[1].ToString();
            LastName = items[2].ToString();
            Phone = items[3] is null ? null : items[3].ToString();
            _position_id = (int)items[4];
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"{Position.Name} {FirstName} {LastName}";
        }
    }
}
