using Npgsql;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using System.Data;

namespace RestaurantsClasses
{
    // класс взаимодействия с базой данных
    public static class Database
    {
        // строка подключения
        private static string _connectionString = "Host=localhost;Username=postgres;Password=root;Database=restaurants";

        // порт сервера
        //private static int _port = 5432;

        // функция соединения с базой данных TODO
        //private static void Connect() { }

        // функция разрыва соединения с базой данных TODO
        //private static void Quit() { }

        //функция получения объектов из базы, где Т - любой наследник класса Model
        public static List<T> GetObject<T>(string where = "", string name = "") where T : Model
        {
            // создаем пустой список объектов
            List<T> objects = new();

            if (name == "")
                name = typeof(T).Name;

            // проверяем, есть ли условие
            string query = $"SELECT * FROM \"{name}\"";
            if (where != "")
            {
                query = $"SELECT * FROM \"{name}\" where {where}";
            }

            // кидаем запрос на выборку
            DataTable table = ExecuteQuery(query);

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in table.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = new object[1];
                parameters[0] = row.ItemArray;

                // создаем новый объект класса Т
                T element = Activator.CreateInstance(typeof(T), parameters) as T;

                // добавляем в список
                objects.Add(element);
            }

            //возвращаем результат
            return objects;
        }

        // обновление авы у пользователя
        //internal static void UploadAvatar(string path, int id) => SendSQL($"UPDATE dbo.[User] SET photo = '{path}' WHERE id = {id}");

        // добавление нового пользователя
        //internal static void AddUser(string username, string password) => SendSQL($"INSERT INTO dbo.[User] VALUES ('{username}', '{password}', '', '')");

        // функция отправки запроса в базу данных
        private static DataTable ExecuteQuery(string query)
        {
            // пустая таблица
            DataTable result = new();

            // пытаемся выполнить кол
            try
            {
                // используя соединение, выполняем дальнейшие команды
                using var connection = new NpgsqlConnection(_connectionString);

                // создаем SQL команду по тексту
                NpgsqlCommand command = new(query, connection);

                // Создаем считывающий элемент
                NpgsqlDataAdapter adapter = new(command);

                // заполняем таблицу
                adapter.Fill(result);
            }

            // если словили ошибку
            catch (Exception ex)
            {
                // закрываем соединение
                Console.WriteLine(ex.Message);
            }

            // возвращаем результат - таблицу
            return result;
        }

        // функция получения ингредиентов по блюду
        public static Dictionary<Ingredient, double> GetIngredients(Meal meal)
        {
            var rawData = ExecuteQuery($"SELECT * FROM \"Ingredient_to_Meal\" WHERE meal_id = {meal.id}");
            var result = new Dictionary<Ingredient, double>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;

                int id = (int)parameters[0];
                var weight = (double)parameters[2];

                // создаем новый объект класса Т
                var ingredient = GetObject<Ingredient>($"id = {id}").FirstOrDefault();

                // добавляем в список
                result.Add(ingredient, weight);
            }
            return result;
        }

        // функция получения ингредиентов по контрагенту
        public static Dictionary<Ingredient, (double weight, double cost)> GetGoods(Kontragent kontragent)
        {
            var rawData = ExecuteQuery($"SELECT * FROM \"Ingredient_to_Kontragent\" WHERE kontragent_id = {kontragent.id}");
            var result = new Dictionary<Ingredient, (double weight, double cost)>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;

                int id = (int)parameters[0];
                var weight = (double)parameters[2];
                var cost = (double)parameters[3];

                // создаем новый объект класса Т
                var ingredient = GetObject<Ingredient>($"id = {id}").FirstOrDefault();

                // добавляем в список
                result.Add(ingredient, (weight, cost));
            }
            return result;
        }

        // функция получения блюд по онлайн заказу
        public static Dictionary<Meal, int> GetMeals(OnlineOrder order)
        {
            var rawData = ExecuteQuery($"SELECT * FROM \"Meal_to_OnlineOrder\" WHERE online_order_id = {order.id}");
            var result = new Dictionary<Meal, int>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;
                 
                int id = (int)parameters[0];
                var count = (int)parameters[2];

                // создаем новый объект класса Т
                var meal = GetObject<Meal>($"id = {id}").FirstOrDefault();

                // добавляем в список
                result.Add(meal, count);
            }
            return result;
        }

        // функция добавления пользователя
        public static Client AddUser(string username, string password)
        {
            var clients = GetObject<Client>();
            int id = 1;
            if (clients.Count > 0)
            {
                id = clients.Last().id + 1;
            }
            ExecuteQuery($"INSERT INTO \"Client\" VALUES ({id}, '{username}', '', '', '{password}')");
            return GetObject<Client>($"id = {id}").FirstOrDefault();
        }
    }
}
