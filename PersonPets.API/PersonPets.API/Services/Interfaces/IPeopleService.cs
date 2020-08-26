using PersonPets.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonPets.API.Services.Interfaces
{
    public interface IPeopleService
    {
        Task<List<PetsResult>> GetPetNamesGroupedUponOwnerGender(string petType);
    }
}
