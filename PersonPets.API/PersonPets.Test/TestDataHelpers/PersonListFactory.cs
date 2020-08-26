using PersonPets.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonPets.Test.TestDataHelpers
{
    public class PersonListFactory
    {
        public List<Person> GetPersonList(PersonListType listType)
        {
            switch (listType)
            {
                case PersonListType.AllGoodData:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 25, Gender = "Male", Name = "Aajay",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Tina", Species = "Cat"},
                                new Pet {Name = "Max", Species = "Dog"},
                                new Pet {Name = "Smokey", Species = "Cat"},
                                new Pet {Name = "Oscar", Species = "Cat"},
                                new Pet {Name = "Fishy", Species = "Fish"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "Female", Name = "Shasha",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Milo", Species = "Cat"},
                                new Pet {Name = "Twister", Species = "Python"},
                            }
                        },
                        new Person
                        {
                            Age = 46, Gender = "Female", Name = "Shashanti",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Rocky", Species = "Dog"},
                                new Pet {Name = "Lucy", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "Male", Name = "Amit",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Mialo", Species = "Cat"},
                                new Pet {Name = "Milon", Species = "Cat"},
                            }
                        },
                    };
                case PersonListType.EmptyList:
                    return new List<Person>();
                case PersonListType.IrregularFontCase:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 25, Gender = "mAlE", Name = "Ajay",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "tINa", Species = "Cat"},
                            }
                        }
                    };
                case PersonListType.MissingPetName:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 45, Gender = "Female", Name = "Sasha",
                            Pets = new List<Pet>
                            {
                                new Pet { Name = "Caty", Species = "Dog" },
                                new Pet { Name = "", Species = "Cat" },
                            }
                        }
                    };
                case PersonListType.AgeLessThanOne:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 45, Gender = "Female", Name = "Sasha",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Caty", Species = "Dog"},
                                new Pet {Name = "", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 0, Gender = "Male", Name = "Sain",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Pssy", Species = "Dog"},
                            }
                        }
                    };
                case PersonListType.InvalidGender:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 45, Gender = "Xyz", Name = "Sasha",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Caty", Species = "Dog"},
                                new Pet {Name = "", Species = "Cat"},
                            }
                        }
                    };
                case PersonListType.MissingPetType:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 45, Gender = "Male", Name = "Sashan",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Caty", Species = ""},
                                new Pet {Name = "", Species = "Cat"},
                            }
                        }
                    };
                case PersonListType.DuplicatePetNames:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 45, Gender = "Male", Name = "Sashan",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Timmy", Species = "Cat"},
                                new Pet {Name = "Timmy", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 45, Gender = "Female", Name = "Silly",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Timmy", Species = "Cat"},
                            }
                        }
                    };
                case PersonListType.PetNamesWithSpaces:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 25, Gender = "Male", Name = "Aajay",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Tina", Species = "Cat"},
                                new Pet {Name = "Max", Species = "Dog"},
                                new Pet {Name = "Smokey", Species = "Cat"},
                                new Pet {Name = "Oscar", Species = "Cat"},
                                new Pet {Name = "Fishy", Species = "Fish"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "Female", Name = "Shasha",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Milo Me", Species = "Cat"},
                                new Pet {Name = "Twister", Species = "Python"},
                            }
                        },
                        new Person
                        {
                            Age = 46, Gender = "Female", Name = "Shashanti",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Rocky", Species = "Dog"},
                                new Pet {Name = "Lucy", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "Male", Name = "Amit",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Ma Mialo", Species = "Cat"},
                                new Pet {Name = "Ma Milon", Species = "Cat"},
                            }
                        },
                    };
                case PersonListType.IrregularGenderCase:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 25, Gender = "Male", Name = "Aajay",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Tina", Species = "Cat"},
                                new Pet {Name = "Max", Species = "Dog"},
                                new Pet {Name = "Smokey", Species = "Cat"},
                                new Pet {Name = "Oscar", Species = "Cat"},
                                new Pet {Name = "Fishy", Species = "Fish"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "Female", Name = "Shasha",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Milo", Species = "Cat"},
                                new Pet {Name = "Twister", Species = "Python"},
                            }
                        },
                        new Person
                        {
                            Age = 46, Gender = "female", Name = "Shashanti",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Rocky", Species = "Dog"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "male", Name = "Amit",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Mialo", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 46, Gender = "feMale", Name = "Shashanti",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Lucy", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 36, Gender = "mALE", Name = "Amit",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Milon", Species = "Cat"},
                            }
                        },

                    };
                case PersonListType.NoPets:
                    return new List<Person>
                    {
                        new Person
                        {
                            Age = 45, Gender = "Male", Name = "Sasha",
                            Pets = new List<Pet>
                            {
                                new Pet {Name = "Caty", Species = "Dog"},
                                new Pet {Name = "Oscar", Species = "Cat"},
                            }
                        },
                        new Person
                        {
                            Age = 50, Gender = "Male", Name = "Sushant",
                            Pets = null
                        }
                    };
                default:
                    return null;
            }

        }

    }
}
