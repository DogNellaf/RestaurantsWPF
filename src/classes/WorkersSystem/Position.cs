using RestaurantsClasses.Enums;

namespace RestaurantsClasses.WorkersSystem
{
    // должность
    public class Position: Model
    {

        // название должности
        public string Name { get; set; }

        // стандартный оклад
        public double Salary { get; set; }

        // процентный размер премии
        public double Prize { get; set; }

        // уровень прав
        public WorkerRole Role { get; set; }

        public Position()
        {

        }

        // конструктор
        public Position(int id, string name, double salary, double prize, WorkerRole role): base(id)
        {
            Name = name;
            Salary = salary;
            Prize = prize;
            Role = role;
        }

        public Position(object[] items) : base((int)items[0])
        {
            Name = items[1].ToString();
            Salary = (double)items[2];
            Prize = (double)items[3];
            Role = (WorkerRole)(int)items[4];
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Должность {Name}, базовый оклад {Salary}, максимальная премия {Prize}%, уровень прав {Role}";
        }
    }
}
