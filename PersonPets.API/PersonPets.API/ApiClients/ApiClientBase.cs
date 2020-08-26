using PersonPets.API.ApiClients.Interfaces;

namespace PersonPets.API.ApiClients
{
    public abstract class ApiClientBase
    {
        public ApiClientBase(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public readonly IApiClient apiClient;
    }
}
