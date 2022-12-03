using RestaurantsClasses.KontragentsSystem;

namespace RestaurantsClasees.OrderSystem
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
        public int ServingsNumber { get; }

        // ингредиенты
        public Dictionary<Ingredient, double> Ingredients { get; }

        // TODO тип блюда
        //public MealType Type { get; }

        // конструктор
        public Meal(int id, string name, double cost, double weight, int servingsNumber, Dictionary<Ingredient, double> ingredients)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Weight = weight;
            ServingsNumber = servingsNumber;
            Ingredients = ingredients;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Блюдо '{Name}', стоит {Cost} р., вес {Weight} г., {ServingsNumber} порций";
        }
    }
}
