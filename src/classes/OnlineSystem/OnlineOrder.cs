using RestaurantsClasees.OrderSystem;

namespace RestaurantsClasses.OnlineSystem
{
    // модель онлайн заказа
    public class OnlineOrder: Model
    {
        private int _client_id;

        // дата заказа
        public DateTime Created { get; }

        // клиент, который заказал
        public Client Client => Database.GetObject<Client>($"id = {_client_id}").FirstOrDefault();

        // список блюд в заказе
        public Dictionary<Meal, int> Meals => Database.GetMeals(this);

        // адрес заказа
        public string Address { get; }

        // завершен ли
        public bool IsComplited { get; private set; }

        // конструктор
        public OnlineOrder(int id, DateTime date, int client_id, string address): base(id)
        {
            Created = date;
            _client_id = client_id;
            Address = address;
            IsComplited = false;
        }

        public OnlineOrder(object[] items) : base((int)items[0])
        {
            Created = (DateTime)items[1];
            _client_id = (int)items[2];
            Address = items[3].ToString();
            IsComplited = (bool)items[4];
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
