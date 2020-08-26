using System;
using System.Collections.Generic;
using FluentValidation;
using PersonPets.API.Models;
using PersonPets.API.Services.Interfaces;

namespace PersonPets.API.Services
{
    public class PeopleValidatorService : AbstractValidator<Person>, IValidatorService<Person>
    {
        public PeopleValidatorService()
        {
            //Validation rules defined here
            RuleFor(x => x.Age).GreaterThan(0).WithMessage(Constants.AgeGreaterThanZero);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Constants.PersonNameEmpty);

            RuleFor(x => x.Gender.ToLower()).Must(IsGenderValid)
                .WithMessage(Constants.InvalidGender);


            RuleForEach(x => x.Pets).Must(p => !string.IsNullOrEmpty(p.Species))
                .WithMessage(Constants.EmptyPetType);

            RuleForEach(x => x.Pets).Must(p => !string.IsNullOrEmpty(p.Name))
                .WithMessage(Constants.EmptyPetName);

        }

        public bool Validate(List<Person> listToValidate, out IList<string> errors)
        {
            errors = new List<string>();
            foreach (var person in listToValidate)
            {
                var validationResults = Validate(person);
                if (!validationResults.IsValid)
                {
                    foreach (var error in validationResults.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    return false;
                }
            }
            return true;
        }

        private bool IsGenderValid(string gender)
        {
            return Constants.SupportedGenders.Contains(gender.ToLower());
        }

    }
}
