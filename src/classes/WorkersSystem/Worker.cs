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

        // роль сотрудника
        public WorkerRole Role { get; }

        // конструктор
        public Worker(int id, string firstName, string lastName, string? phone, WorkerRole role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Role = role;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"{Role} {FirstName} {LastName}";
        }
    }
}
