using Npgsql;
using RestaurantsClasses;
using System.Data;

namespace RestaurantsDataApi.Helpers
{
    // класс взаимодействия с базой данных
    public static class Database
    {
        // строка подключения
        private static string _connectionString = "Host=localhost;Username=postgres;Password=root;Database=restaurants";

        // порт сервера
        private static int _port = 5432;

        // функция соединения с базой данных TODO
        private static void Connect() { }

        // функция разрыва соединения с базой данных TODO
        private static void Quit() { }

        //функция получения объектов из базы, где Т - любой наследник класса Model
        internal static List<T> GetObject<T>(string where = "") where T : Model
        {
            // создаем пустой список объектов
            List<T> objects = new();

            // проверяем, есть ли условие
            string query = $"SELECT * FROM \"{typeof(T).Name}\"";
            if (where != "")
            {
                query = $"SELECT * FROM \"{typeof(T).Name}\" where {where}";
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
    }
}
