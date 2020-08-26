using PersonPets.API.Models;
using PersonPets.API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using PersonPets.API.Extensions;
using PersonPets.API.ApiClients.Interfaces;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Text;

namespace PersonPets.API.Services
{
    public class PeopleService : IPeopleService 
    {
        private readonly IValidatorService<Person> _validatorService;
        private readonly IPeopleApiClient _peopleApiClient;
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(IPeopleApiClient peopleApiClient,  IValidatorService<Person> validatorService, ILogger<PeopleService> logger)
        {
            _peopleApiClient = peopleApiClient;
            _validatorService = validatorService;
            _logger = logger;
        }
        public async Task<List<PetsResult>> GetPetNamesGroupedUponOwnerGender(string petType)
        {

            List<Person> persons = await _peopleApiClient.GetPersonData();

            var isValid = _validatorService.Validate(persons, out IList<string> errors);
            if (!isValid)
            {
                _logger.LogError("Person Pets data validation Failed.");
                foreach (var validationError in errors)
                {
                    _logger.LogError(validationError);
                }
                throw new ValidationException("Person Pets data validation Failed.");
            }

            var ownerPetsData =
                (from person in persons.Where(p => p.Pets != null)
                 from pet in person.Pets.Where(p => p.Species.Trim().ToLower() == petType.ToLower())
                 select new
                 {
                     Gender = person.Gender.ToTitleCase(),
                     PetName = pet.Name.ToTitleCase()
                 })
                .OrderByDescending(d => d.Gender)
                .ThenBy(d => d.PetName)
                .GroupBy(p => p.Gender, p => p.PetName,
                    (ownerGender, petNames) => new PetsResult()
                    {
                        OwnerGender = ownerGender,
                        PetNames = petNames.ToList()
                    }).ToList();

            if (ownerPetsData.Count < 2)
            {
                if (ownerPetsData.Count == 1)
                {
                    var isExistingItemForMale = ownerPetsData[0].OwnerGender == Constants.Male;
                    ownerPetsData.Insert(
                        isExistingItemForMale ? 1 : 0,
                        new PetsResult
                        {
                            OwnerGender = isExistingItemForMale ? Constants.Female : Constants.Male,
                            PetNames = new List<string>()
                        });
                }
                else
                {
                    ownerPetsData.Add(new PetsResult { OwnerGender = Constants.Male, PetNames = new List<string>() });
                    ownerPetsData.Add(new PetsResult { OwnerGender = Constants.Female, PetNames = new List<string>() });
                }
            }
            return ownerPetsData;
        }

    }
}
