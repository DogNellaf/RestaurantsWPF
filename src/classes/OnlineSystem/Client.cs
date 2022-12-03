namespace RestaurantsClasses.OnlineSystem
{
    // клиент
    public class Client: Model
    {

        // имя пользователя для входа
        public string Username { get; }

        // имя
        public string FirstName { get; }

        // фамилия
        public string SecondName { get; }

        // хэш пароля
        public string Password { get; }

        // конструктор
        public Client(int id, string username, string firstName, string secondName, string password): base(id)
        {
            Username = username;
            FirstName = firstName;
            SecondName = secondName;
            Password = password;
        }

        public Client(object[] items) : base((int)items[0])
        {
            Username = items[1].ToString();
            FirstName = items[2].ToString();
            SecondName = items[3].ToString();
            Password = items[4].ToString();
        }

        // функция авторизации
        public bool Auth(string password) => Password == GetHash(password);

        // текстовый вывод
        public override string ToString()
        {
            return $"Пользователь {Username}";
        }

        // функция получения хэша пароля
        private string GetHash(string password)
        {
            // TODO: сделать шифрование
            return password;
        }
    }
}
