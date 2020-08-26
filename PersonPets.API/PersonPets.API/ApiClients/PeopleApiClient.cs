using PersonPets.API.ApiClients.Interfaces;
using PersonPets.API.Models;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonPets.API.ApiClients
{
    public class PeopleApiClient : ApiClientBase, IPeopleApiClient
    {
        private readonly ILogger _logger;
    
        public PeopleApiClient(IApiClient apiClient) : base(apiClient)
        {
            _logger = Log.ForContext(typeof(PeopleApiClient));
        }

        public async Task<List<Person>> GetPersonData()
        {
            return await apiClient.GetAsync<List<Person>>("people.json");
        }
    }
}
