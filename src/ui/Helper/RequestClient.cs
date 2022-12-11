using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestaurantsClasees;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses;
using RestaurantsClasses.OnlineSystem;
using RestaurantsClasses.WorkersSystem;

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

            return JsonConvert.DeserializeObject<List<T>>(raw);
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
                return JsonConvert.DeserializeObject<Client>(result);
            }
            catch
            {
                return null;
            }
           
        }

        // проверка авторизации сотрудника 
        public static Worker AuthWorker(string username, string password)
        {
            var result = SendRequest($"api/authworker?username={username}&password={password}");

            try
            {
                return JsonConvert.DeserializeObject<Worker>(result);
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

            return JsonConvert.DeserializeObject<Client>(result);
        }

        // получение заказов по пользователю
        public static List<OnlineOrder> GetOnlineOrdersByClient(Client client)
        {
            var result = SendRequest($"api/onlineorders?client_id={client.id}");

            return JsonConvert.DeserializeObject<List<OnlineOrder>>(result);
        }

        // получение заказов по пользователю
        public static List<OfflineOrder> GetAllOfflineOrders()
        {
            var result = SendRequest($"api/offlineorders");

            return JsonConvert.DeserializeObject<List<OfflineOrder>>(result);
        }

        // получение заказов по пользователю
        public static List<OfflineOrder> NewOrders()
        {
            var result = SendRequest($"api/neworders");

            return JsonConvert.DeserializeObject<List<OfflineOrder>>(result);
        }

        // получение названия должности по id
        public static string GetPositionName(int id) => SendRequest($"api/getpositionname?id={id}");

        // получение уровня доступа по id должности
        public static bool CheckIsItAdmin(int id) => bool.Parse(SendRequest($"api/isitadmin?id={id}"));

        // закрепить столик за работником
        public static void SetOrderToWorker(int order_id, int worker_id) => SendRequest($"api/setordertoworker?order_id={order_id}&worker_id={worker_id}");
    }
}
