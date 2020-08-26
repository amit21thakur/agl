using Newtonsoft.Json;

namespace PersonPets.API.Models
{
    public class Pet
    {
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Species { get; set; }

    }
}
