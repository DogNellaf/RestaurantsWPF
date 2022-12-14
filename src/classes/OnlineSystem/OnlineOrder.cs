using RestaurantsClasees.OrderSystem;

namespace RestaurantsClasses.OnlineSystem
{
    // модель онлайн заказа
    public class OnlineOrder: Model
    {
        public int ClientId { get; set; }

        // дата заказа
        public DateTime created { get; set; }

        // клиент, который заказал
       // public Client GetClient() => Database.GetObject<Client>($"id = {_client_id}").FirstOrDefault();

        // список блюд в заказе
        //public Dictionary<Meal, int> GetMeals() => Database.GetMeals(this);

        // адрес заказа
        public string address { get; set; }

        // завершен ли
        public bool isComplited { get; set; }

        // конструктор
        public OnlineOrder(int id, DateTime date, int client_id, string address): base(id)
        {
            created = date;
            ClientId = client_id;
            this.address = address;
            isComplited = false;
        }

        public OnlineOrder(object[] items) : base((int)items[0])
        {
            created = (DateTime)items[1];
            ClientId = (int)items[2];
            address = items[3].ToString();
            isComplited = (bool)items[4];
        }

        public OnlineOrder()
        {

        }

        // отметить заказ завершенным
        public void Finish() => isComplited = true;

        // текстовый вывод
        //public override string ToString()
        //{
        //    return $"Онлайн заказ от {created}, заказал {client.FirstName} {client.SecondName} на адрес {address}";
        //}
    }
}
