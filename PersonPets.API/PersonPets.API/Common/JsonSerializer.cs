using Newtonsoft.Json;
using PersonPets.API.Common.Interfaces;

namespace PersonPets.API.Common
{
    public class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}