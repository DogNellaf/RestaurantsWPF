namespace RestaurantsClasses.KontragentsSystem
{
    // ингредиент 
    public class Ingredient
    {
        // id ингредиента из базы
        public int Id { get; }

        // название
        public string Name { get; }

        // стоимость
        // public double Cost { get; }

        // вес
        // public double Weight { get; }

        // конструктор
        public Ingredient(int id, string name)
        {
            Id = id;
            Name = name;
            // Cost = cost;
            // Weight = weight;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Ингредиент '{Name}'";
        }
    }
}
