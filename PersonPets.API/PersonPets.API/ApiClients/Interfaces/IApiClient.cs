using System.Threading.Tasks;

namespace PersonPets.API.ApiClients.Interfaces
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string uri);
    }
}