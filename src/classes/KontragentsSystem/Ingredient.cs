namespace RestaurantsClasses.KontragentsSystem
{
    // ингредиент 
    public class Ingredient: Model
    {

        // название
        public string Name { get; set; }

        public Ingredient()
        {

        }

        // конструктор
        public Ingredient(int id, string name): base(id)
        {
            Name = name;
        }

        public Ingredient(object[] items) : base((int)items[0])
        {
            Name = items[1].ToString();
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Ингредиент '{Name}'";
        }
    }
}
