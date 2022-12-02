namespace RestaurantsClasses.KontragentsSystem
{
    // класс компании - поставщика ингредиентов 
    public class Kontragent: Model
    {

        // название компании
        public string Name { get; }

        // адрес
        public string Address { get; }

        // конструктор
        public Kontragent(int id, string name, string address): base(id)
        {
            Name = name;
            Address = address;
        }

        // текстовый вывод
        public override string ToString()
        {
            return $"Компания '{Name}', адрес - {Address}";
        }
    }
}
