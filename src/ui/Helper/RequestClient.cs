using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestaurantsClasees;
using RestaurantsClasses;
using RestaurantsClasses.OnlineSystem;

namespace ui.Helper
{
    internal static class RequestClient
    {
        // адрес сервера
        private static string _server = "https://localhost:7173";

        // порт
        //private static int _port = 7173;

        // функция отправки запроса на сервер и получения списка объектов
        public static List<T> GetObjects<T>() where T: Model
        {
            string raw = SendRequest($"{typeof(T).Name}");

            return JsonSerializer.Deserialize<List<T>>(raw);
        }

        private static string SendRequest(string url)
        {
            var request = WebRequest.Create($"{_server}/{url}");

            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();

            var reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
        }

        // проверка авторизации пользователя 
        public static Client Auth(string username, string password)
        {
            var result = SendRequest($"api/auth?username={username}&password={password}");

            try
            {
                return JsonSerializer.Deserialize<Client>(result);
            }
            catch
            {
                return null;
            }
           
        }

        // регистрация
        public static Client Register(string username, string password)
        {
            var result = SendRequest($"api/adduser?username={username}&password={password}");

            return JsonSerializer.Deserialize<Client>(result);
        }
    }
}
