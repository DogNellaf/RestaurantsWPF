namespace RestaurantsClasses.KontragentsSystem
{
    // класс компании - поставщика ингредиентов 
    public class Kontragent: Model
    {

        // название компании
        public string Name { get; }

        // адрес
        public string Address { get; }

        // какие предлагает ингредиенты 
        public Dictionary<Ingredient, double> Goods;

        // конструктор
        public Kontragent(int id, string name, string address, Dictionary<Ingredient, double> goods) : base(id)
        {
            Name = name;
            Address = address;
            Goods = goods;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Компания '{Name}', адрес - {Address}";
        }
    }
}
