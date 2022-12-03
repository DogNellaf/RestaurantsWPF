using RestaurantsClasees.OrderSystem;

namespace RestaurantsClasses.OnlineSystem
{
    // модель онлайн заказа
    public class OnlineOrder: Model
    {
        // дата заказа
        public DateTime Created { get; }

        // клиент, который заказал
        public Client Client { get; }

        // список блюд в заказе
        public Dictionary<Meal, int> Meals { get; }

        // адрес заказа
        public string Address { get; }

        // завершен ли
        public bool IsComplited { get; private set; }

        // конструктор
        public OnlineOrder(int id, DateTime date, Client client, Dictionary<Meal, int> meals, string address): base(id)
        {
            Created = date;
            Client = client;
            Meals = meals;
            Address = address;
        }

        // отметить заказ завершенным
        public void Finish() => IsComplited = true;

        // текстовый вывод
        public override string ToString()
        {
            return $"Онлайн заказ от {Created}, заказал {Client.FirstName} {Client.SecondName} на адрес {Address}";
        }
    }
}
