namespace RestaurantsClasses.KontragentsSystem
{
    // ингредиент 
    public class Ingredient: Model
    {

        // название
        public string Name { get; }

        // конструктор
        public Ingredient(int id, string name): base(id)
        {
            Name = name;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Ингредиент '{Name}'";
        }
    }
}
