namespace RestaurantsDataApi.Models
{
    // класс компании - поставщика ингредиентов 
    public class Kontragent
    {
        // id ингредиента из базы
        public int Id { get; }

        // название компании
        public string Name { get; }

        // адрес
        public string Address { get; }

        // конструктор
        public Kontragent(int id, string name, string address)
        {
            Id = id;
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
