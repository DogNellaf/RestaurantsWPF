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

        public Meal(int _id, string _name, double _cost, double weight, double servingsNumber)
        {
            Id = _id;
            Name = _name;
            Cost = _cost;
            Weight = weight;
            ServingsNumber = servingsNumber;
        }

        public override string ToString()
        {
            return $"Блюдо '{Name}', стоит {Cost} р., вес {Weight} г., {ServingsNumber} порций";
        }
    }
}
