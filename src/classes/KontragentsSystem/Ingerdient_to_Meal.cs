using RestaurantsClasees.OrderSystem;

namespace RestaurantsClasses.KontragentsSystem
{
    // ингредиент в блюде
    public class Ingerdient_to_Meal
    {
        // заказ
        public Ingredient Ingredient { get; }

        // блюдо
        public Meal Meal { get; }

        // вес
        public double Weight { get; }

        // конструктор
        public Ingerdient_to_Meal(Ingredient ingredient, Meal meal, double weight)
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
