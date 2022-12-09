using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using RestaurantsClasses.WorkersSystem;
using RestaurantsClasses.Enums;

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

        public IEnumerable<Ingredient> GetIngredientsByMeal(int meal_id)
        {
            var meal = Database.GetObject<Meal>($"id = {meal_id}").FirstOrDefault();

            if (meal is null)
                return new List<Ingredient>();

            return meal.GetIngredients().Select(x => x.Key);
        }

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

        public List<OnlineOrder> OnlineOrders(int client_id)
        {
            return Database.GetObject<OnlineOrder>($"client_id = {client_id}");
        }


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

            return position.Role == WorkerRole.Администратор;
        }
    }
}