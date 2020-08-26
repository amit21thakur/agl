using System.Collections.Generic;

namespace PersonPets.API.Models
{
    public class Person
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public IList<Pet> Pets { get; set; }

    }
}
