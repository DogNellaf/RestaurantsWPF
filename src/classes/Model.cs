using Newtonsoft.Json;

namespace RestaurantsClasses
{
    // базовый общий класс для всего
    public abstract class Model
    {
        [JsonProperty("id")]
        public virtual int id { get; set; }

        public Model(int id)
        {
            this.id = id;
        }

        public Model()
        {

        }
    }
}
