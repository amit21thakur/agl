using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonPets.API.Models
{
    public class PetsResult
    {
        public string OwnerGender { get; set; }

        public List<string> PetNames { get; set; }

        public bool Equals(PetsResult petsResult)
        {
            var areSame = petsResult != null && string.Compare(petsResult.OwnerGender, OwnerGender, true) == 0;
            if (areSame)
            {
                if (petsResult.PetNames.Count != PetNames.Count)
                    return false;

                for (int i = 0; i < PetNames.Count; i++)
                {
                    areSame = areSame && string.Compare(petsResult.PetNames[i], PetNames[i], true) == 0;
                }
            }
            return areSame;
        }
    }
}
