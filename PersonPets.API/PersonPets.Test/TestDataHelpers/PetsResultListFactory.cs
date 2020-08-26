using PersonPets.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonPets.Test.TestDataHelpers
{
    public class PetsResultListFactory
    {
        public List<PetsResult> GetPetsResultList(PersonListType listType)
        {
            switch (listType)
            {
                case PersonListType.AllGoodData:
                    return new List<PetsResult>
                    {
                        new PetsResult
                        {
                            OwnerGender = "Male",
                            PetNames = new List<string>
                            {
                                "Mialo",
                                "Milon",
                                "Oscar",
                                "Smokey",
                                "Tina"
                            }
                        },
                        new PetsResult
                        {
                            OwnerGender = "Female",
                            PetNames = new List<string>
                            {
                                "Lucy",
                                "Milo"
                            }
                        }
                    };
                case PersonListType.PetNamesWithSpaces:
                    return new List<PetsResult>
                    {
                        new PetsResult
                        {
                            OwnerGender = "Male",
                            PetNames = new List<string>
                            {
                                "Ma Mialo",
                                "Ma Milon",
                                "Oscar",
                                "Smokey",
                                "Tina"
                            }
                        },
                        new PetsResult
                        {
                            OwnerGender = "Female",
                            PetNames = new List<string>
                            {
                                "Lucy",
                                "Milo Me"
                            }
                        }
                    };
                default:
                    return null;
            }

        }
    }
}
