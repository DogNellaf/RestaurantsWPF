using Microsoft.AspNetCore.Mvc;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses;
using RestaurantsClasses.KontragentsSystem;

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

        public bool Auth(string login, string password, bool isClient = false)
        {
            if (isClient)
            {

            }
        }
    }
}