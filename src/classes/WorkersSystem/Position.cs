using RestaurantsClasses.Enums;

namespace RestaurantsClasses.WorkersSystem
{
    // должность
    public class Position
    {
        // id из базы
        public int Id { get; }

        // название должности
        public string Name { get; }

        // стандартный оклад
        public double Salary { get; }

        // процентный размер премии
        public double Prize { get; }

        // уровень прав
        public WorkerRole Role { get; }

        // конструктор
        public Position(int id, string name, double salary, double prize, WorkerRole role)
        {
            Id = id;
            Name = name;
            Salary = salary;
            Prize = prize;
            Role = role;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Должность {Name}, базовый оклад {Salary}, максимальная премия {Prize}%, уровень прав {Role}";
        }
    }
}
