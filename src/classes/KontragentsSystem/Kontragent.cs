namespace RestaurantsClasses.KontragentsSystem
{
    // класс компании - поставщика ингредиентов 
    public class Kontragent: Model
    {

        // название компании
        public string Name { get; set; }

        // адрес
        public string Address { get; set; }

        // какие предлагает ингредиенты 
        //public Dictionary<Ingredient, (double weight, double cost)> Goods => Database.GetGoods(this);

        // конструктор
        public Kontragent(int id, string name, string address) : base(id)
        {
            Name = name;
            Address = address;
        }

        public Kontragent(object[] items) : base((int)items[0])
        {
            Name = items[1].ToString();
            Address = items[2].ToString();
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Компания '{Name}', адрес - {Address}";
        }
    }
}
