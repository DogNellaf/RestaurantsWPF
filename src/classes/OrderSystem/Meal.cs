using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses;

namespace RestaurantsClasees.OrderSystem
{
    // блюдо
    public class Meal: Model
    {

        // название
        public string Name { get; set; }

        // стоимость
        public double Cost { get; set; }

        // вес
        public double Weight { get; set; }

        // количество порций
        public int ServingsNumber { get; set; }

        // ингредиенты
        public Dictionary<Ingredient, double> GetIngredients() => Database.GetIngredients(this);

        // конструктор
        public Meal(int id, string name, double cost, double weight, int servingsNumber): base(id)
        {
            Name = name;
            Cost = cost;
            Weight = weight;
            ServingsNumber = servingsNumber;
        }

        public Meal(object[] items): base((int)items[0])
        {
            Name = items[1].ToString();
            Cost = (double)items[2];
            Weight = (double)items[3];
            ServingsNumber = (int)items[4];
        }

        public Meal()
        {

        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Блюдо '{Name}', стоит {Cost} р., вес {Weight} г., {ServingsNumber} порций";
        }
    }
}
