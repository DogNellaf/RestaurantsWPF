namespace RestaurantsClasses.OnlineSystem
{
    // клиент
    public class Client
    {
        // id из базы
        public int Id { get; }

        // имя пользователя для входа
        public string Username { get; }

        // имя
        public string FirstName { get; }

        // фамилия
        public string SecondName { get; }

        // хэш пароля
        public string Password { get; }

        // конструктор
        public Client(int id, string username, string firstName, string secondName, string password)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            SecondName = secondName;
            Password = password;
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
