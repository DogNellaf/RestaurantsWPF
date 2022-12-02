namespace RestaurantsClasses.KontragentsSystem
{
    public class Ingredient_to_Konragents: Model
    {
        // заказ
        public Ingredient Ingredient { get; }

        // блюдо
        public Kontragent Kontragent { get; }

        // вес
        public double Weight { get; }

        // стоимость
        public double Cost { get; }

        // конструктор
        public Ingredient_to_Konragents(int id, Ingredient ingredient, Kontragent kontragent, double weight, double cost): base(id)
        {
            if (ingredient is null)
            {
                throw new Exception("Ингредиент обязан существовать");
            }

            if (kontragent is null)
            {
                throw new Exception("Контрагент обязан существовать");
            }

            if (weight <= 0)
            {
                throw new Exception("Вес предложения у контрагента не может быть отрицательным или равен нулю");
            }

            if (weight < 0)
            {
                throw new Exception("Цена ингредиента у контрагента не может быть отрицательной");
            }

            Ingredient = ingredient;
            Kontragent = kontragent;
            Weight = weight;
            Cost = cost;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Компания {Kontragent.Name} предлагает {Weight} г. {Ingredient.Name} по цене {Cost} за 1 г.";
        }
    }
}
