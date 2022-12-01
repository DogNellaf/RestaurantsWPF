namespace RestaurantsDataApi.Models
{
    // блюдо
    public class Meal
    {
        // id блюда из базы
        public int Id { get; }

        // название
        public string Name { get; }

        // стоимость
        public double Cost { get; }

        // вес
        public double Weight { get; }

        // количество порций
        public double ServingsNumber { get; }

        // TODO тип блюда
        //public MealType Type { get; }

        public Meal(int id, string name, double cost, double weight, double servingsNumber)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Weight = weight;
            ServingsNumber = servingsNumber;
        }

        public override string ToString()
        {
            return $"Блюдо '{Name}', стоит {Cost} р., вес {Weight} г., {ServingsNumber} порций";
        }
    }
}
