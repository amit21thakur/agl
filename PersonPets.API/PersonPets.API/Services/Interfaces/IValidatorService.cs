using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonPets.API.Services.Interfaces
{
    public interface IValidatorService<T>
    {
        /// <summary>
        /// Validates the List of given type 
        /// </summary>
        /// <param name="listToValidate">list of objects to be validated</param>
        /// <param name="errors">list of validation errors</param>
        /// <returns>true, if no errors found. Else false</returns>
        bool Validate(List<T> listToValidate, out IList<string> errors);
    }
}
