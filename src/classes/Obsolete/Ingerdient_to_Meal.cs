using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;

namespace RestaurantsClasses.Obsolete
{
    // ингредиент в блюде
    [Obsolete]
    public class Ingerdient_to_Meal : Model
    {
        // заказ
        public Ingredient Ingredient { get; set; }

        // блюдо
        public Meal Meal { get; set; }

        // вес
        public double Weight { get; set; }

        // конструктор
        public Ingerdient_to_Meal(int id, Ingredient ingredient, Meal meal, double weight) : base(id)
        {
            if (ingredient is null)
            {
                throw new Exception("Ингредиент обязан существовать");
            }

            if (meal is null)
            {
                throw new Exception("Блюдо обязано существовать");
            }

            if (weight <= 0)
            {
                throw new Exception("Вес ингредиента в блюде не может быть отрицательным или равен нулю");
            }

            Ingredient = ingredient;
            Meal = meal;
            Weight = weight;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"В блюде {Meal.Name} есть {Weight} г. {Ingredient.Name}";
        }
    }
}
