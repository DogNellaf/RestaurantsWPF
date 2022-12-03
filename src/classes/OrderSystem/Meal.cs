using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses;

namespace RestaurantsClasees.OrderSystem
{
    // блюдо
    public class Meal: Model
    {

        // название
        public string Name { get; }

        // стоимость
        public double Cost { get; }

        // вес
        public double Weight { get; }

        // количество порций
        public int ServingsNumber { get; }

        // ингредиенты
        public Dictionary<Ingredient, double> Ingredients { get; private set; }

        // TODO тип блюда
        //public MealType Type { get; }

        // конструктор
        public Meal(int id, string name, double cost, double weight, int servingsNumber, Dictionary<Ingredient, double> ingredients): base(id)
        {
            Name = name;
            Cost = cost;
            Weight = weight;
            ServingsNumber = servingsNumber;
            Ingredients = ingredients;
        }

        public Meal(object[] items): base((int)items[0])
        {
            Name = items[1].ToString();
            Cost = (double)items[2];
            Weight = (double)items[3];
            ServingsNumber = (int)items[4];
        }

        // добавить ингредиенты блюду
        public void SetIngredients(Dictionary<Ingredient, double> ingredients)
        {
            Ingredients = ingredients;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Блюдо '{Name}', стоит {Cost} р., вес {Weight} г., {ServingsNumber} порций";
        }
    }
}
