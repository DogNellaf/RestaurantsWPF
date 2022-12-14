using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using RestaurantsClasses.WorkersSystem;
using RestaurantsClasses.Enums;
using System.Numerics;
using System.ComponentModel;
using System.Xml.Linq;
using RestaurantsClasses.BookingSystem;
using Microsoft.VisualBasic;

namespace RestaurantsDataApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        public IList<Meal> GetMeals()
        {
            return Database.GetObject<Meal>();
        }


        public IEnumerable<Ingredient> GetIngredients()
        {
            return Database.GetObject<Ingredient>();
        }

        public string GetOfflineMeals(int order_id)
        {
            var rawMeals = Database.GetOfflineMeals(order_id);
            return JsonConvert.SerializeObject(rawMeals);
        }

        //public IEnumerable<Ingredient> GetIngredientsByMeal(int meal_id)
        //{
        //    var meal = Database.GetObject<Meal>($"id = {meal_id}").FirstOrDefault();

        //    if (meal is null)
        //        return new List<Ingredient>();

        //    return meal.GetIngredients().Select(x => x.Key);
        //}

        public Client Auth(string username, string password)
        {
            var client = Database.GetObject<Client>($"username = '{username}'").FirstOrDefault();

            if (client is null)
                return null;

            if (Encoder.CheckHash(password, client.Password))
                return client;

            return null;
        }

        public string AuthWorker(string username, string password)
        {
            var worker = Database.GetObject<Worker>($"username = '{username}'").FirstOrDefault();

            if (worker is null)
                return null;

            if (Encoder.CheckHash(password, worker.Password))
                return JsonConvert.SerializeObject(worker);

            return null;
        }

        public Client AddUser(string username, string password)
        {
            var client = Database.GetObject<Client>($"username = {username}").FirstOrDefault();

            if (client is not null)
                return null;

            var hash = Encoder.Encode(password);

            return Database.AddUser(username, hash);
        }

        public List<OfflineOrder> OfflineOrders() => Database.GetObject<OfflineOrder>("", "Order");
        public List<Ingredient> GetIngredientsByMeal(int id) => Database.GetIngredientsByMeal(id);

        public List<OnlineOrder> OnlineOrders(int client_id) => Database.GetObject<OnlineOrder>($"client_id = {client_id}");

        public List<OfflineOrder> NewOrders() => Database.GetObject<OfflineOrder>($"status_id = {1}", "Order");

        public List<Worker> GetWorkers() => Database.GetObject<Worker>();
        public List<Position> GetPositions() => Database.GetObject<Position>();
        public List<Table> GetTables() => Database.GetObject<Table>();

        public string GetPositionName(int id)
        {
            var position = Database.GetObject<Position>($"id = {id}").FirstOrDefault();
            if (position is null)
                return string.Empty;

            return position.Name;
        }

        public bool IsItAdmin(int id)
        {
            var position = Database.GetObject<Position>($"id = {id}").FirstOrDefault();
            if (position is null)
                return false;

            return position.Role == WorkerRole.Admin;
        }

        public void SetOrderToWorker(int order_id, int worker_id) => Database.SetOrderToWorker(order_id, worker_id);

        public void SetOrderComplete(int order_id) => Database.SetOrderComplete(order_id);

        public void DeliverOfflineMeal(int order_id, int meal_id) => Database.DeliverOfflineMeal(order_id, meal_id);

        public string GenerateNewPassword(int worker_id, int admin_id) => Database.GenerateNewPassword(worker_id, admin_id);

        public void CreateWorker(string username, string firstName, string secondName, long phone) => Database.CreateWorker(username, firstName, secondName, phone);

        public void UpdateWorker(int worker_id, string username, string firstName, string secondName, long phone) => Database.UpdateWorker(worker_id, username, firstName, secondName, phone);

        public void CreateMeal(string name, float cost, float weight, int servnumber) => Database.CreateMeal(name, cost, weight, servnumber);

        public void UpdateMeal(int meal_id, string name, float cost, float weight, int servnumber) => Database.UpdateMeal(meal_id, name, cost, weight, servnumber);

        public void CreateIngredient(string name) => Database.CreateIngredient(name);

        public void UpdateIngredient(int id, string name) => Database.UpdateIngredient(id, name);

        public void DeleteIngredient(int id) => Database.DeleteIngredient(id);

        public void Delete(string name, int id) => Database.Delete(name, id);

        public void AddIngredientsToMeal(int meal_id, int ingredient_id) => Database.AddIngredientsToMeal(meal_id, ingredient_id);

        public void DeleteIngredientByMeal(int meal_id, int id) => Database.DeleteIngredientByMeal(meal_id, id);

        public List<OnlineOrder> GetOnlineOrders() => Database.GetObject<OnlineOrder>($"is_complited = false");

        public void CreateOnlineOrder(int client_id, string address) => Database.CreateOnlineOrder(client_id, address);

        public void SetOnlineOrderComplete(int order_id) => Database.SetOnlineOrderComplete(order_id);

        public void UpdatePosition(int worker_id, int position_id) => Database.UpdateWorkerPosition(worker_id, position_id);
        public void CreateOfflineOrder(int table_id, string meals) => Database.CreateOfflineOrder(table_id, meals);
        public List<Meal> GetOnlineMeals(int id) => Database.GetOnlineMeals(id);
        public void AddMealToOnlineOrder(int meal_id, int order_id) => Database.AddMealToOnlineOrder(meal_id, order_id);
        public void RemoveMealToOnlineOrder(int meal_id, int order_id) => Database.RemoveMealToOnlineOrder(meal_id, order_id);
        public List<double> GetMonthData()
        {
            var now = DateTime.Now;
            var date = DateTime.Parse($"{now.Year}-{now.Month}-{1}");
            var offlineOrders = Database.GetObject<OfflineOrder>("", "Order");
            var onlineOrders = Database.GetObject<OnlineOrder>();

            var result = new List<double>();

            while (date.Month == now.Month)
            {
                double sum = 0;
                var offlineOrdersByDay = offlineOrders.Where(x => x.Created.Date == date.Date);
                var onlineOrdersByDay = onlineOrders.Where(x => x.created.Date == date.Date);

                foreach (var offline in offlineOrdersByDay)
                {
                    sum += Database.GetOfflineMeals(offline.id).Sum(x => x.Cost);
                }

                foreach (var offline in onlineOrdersByDay)
                {
                    sum += Database.GetOnlineMeals(offline.id).Sum(x => x.Cost);
                }
                result.Add(sum);
                date = date.AddDays(1);
            }
            return result;
        }

        //public void UpdateOnlineOrder(int id, string address) => Database.UpdateOnlineOrder(id, address);
    }
}