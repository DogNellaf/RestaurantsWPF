using Microsoft.AspNetCore.Mvc;

namespace RestaurantsDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {

        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRestaurants")]
        public IEnumerable<WeatherForecast> Get()
        {
            return null;
        }
    }
}