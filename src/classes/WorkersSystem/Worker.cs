using RestaurantsClasses.Enums;

namespace RestaurantsClasses.WorkersSystem
{
    // сотрудник
    public class Worker
    {
        // Id из базы
        public int Id { get; }

        // имя
        public string FirstName { get; }

        // фамилия 
        public string LastName { get; }

        // телефон 
        public string? Phone { get; }

        // должность сотрудника
        public Position Position { get; }

        // конструктор
        public Worker(int id, string firstName, string lastName, string? phone, Position position)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Position = position;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"{Position.Name} {FirstName} {LastName}";
        }
    }
}
