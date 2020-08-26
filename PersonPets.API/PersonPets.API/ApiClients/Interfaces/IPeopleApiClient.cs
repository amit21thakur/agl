using PersonPets.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonPets.API.ApiClients.Interfaces
{
    public interface IPeopleApiClient
    {
        Task<List<Person>> GetPersonData();
    }
}
