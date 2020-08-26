using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonPets.API
{
    public class Constants
    {
        public const string HostUrl = "HostUrl";

        public const string AgeGreaterThanZero = "The Age must be at greater than 0.";

        public const string PersonNameEmpty = "The Person Name cannot be empty.";

        public const string InvalidGender = "The Gender must have a valid value.";

        public const string EmptyPetType = "The pet type cannot be empty";

        public const string EmptyPetName = "The pet name cannot be empty";

        public const string CorsUrl = "CorsUrl";
        public const string Cat = "cat";
        public const string Male = "Male";
        public const string Female = "Female";

        //At the moment, we are supporting only two genders
        public static readonly HashSet<string> SupportedGenders = new HashSet<string>
        {
            "male",
            "female"
        };
    }
}
