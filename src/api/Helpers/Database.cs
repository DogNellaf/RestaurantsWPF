using System.Data;

namespace RestaurantsDataApi.Helpers
{
    // класс взаимодействия с базой данных
    public static class Database
    {
        // строка подключения
        private static string _connectionString = "";

        // порт сервера
        private static int _port = 5432;

        // функция соединения с базой данных TODO
        private static void Connect() { }

        // функция разрыва соединения с базой данных TODO
        private static void Quit() { }

        // функция отправки запроса в базу данных TODO
        private static DataTable ExecuteQuery(string query) { return null;  }
    }
}
