using Microsoft.AspNetCore.Mvc;
using RestaurantsClasees.OrderSystem;
using RestaurantsDataApi.Helpers;

namespace RestaurantsDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMeals")]
        public IEnumerable<Meal> Get()
        {
            return Database.GetObject<Meal>();
        }
    }
}