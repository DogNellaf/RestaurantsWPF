namespace RestaurantsDataApi.Models
{
    public class Meal
    {
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

        public Meal(string _name, double _cost, double weight, double servingsNumber)
        {
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
