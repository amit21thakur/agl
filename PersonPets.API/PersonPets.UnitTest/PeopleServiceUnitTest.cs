using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonPets.API.Models;
using System.Collections.Generic;
using PersonPets.API;
using PersonPets.API.Services.Interfaces;
using PersonPets.API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using PersonPets.API.ApiClients.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace PersonPets.UnitTest
{
    /// <summary>
    /// Unit test cases for Processor
    /// </summary>
    [TestClass]
    public class PeopleServiceUnitTest
    {
        private ILogger<PeopleService> _logger = (new Mock<ILogger<PeopleService>>()).Object;

        private IPeopleService GetMockedService(List<Person> persons)
        {
            var apiMock = new Mock<IPeopleApiClient>();
            apiMock.Setup(apiClient => apiClient.GetPersonData()).Returns(Task.FromResult(persons));
            return new PeopleService(apiMock.Object, new ValidatorService(), _logger);
        }

        /// <summary>Process should ignore persons having no pets</summary>
        [TestMethod]
        public void Process_should_ignore_persons_having_no_pets()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="Male", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
                new Person { Age = 20, Name="Raj", Gender="Female", Pets = null},
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.AreEqual(petsResults.Sum(x => x.PetNames.Count), 1);
        }

        /// <summary>Test for Title case of Gender</summary>
        [TestMethod]
        public void Test_for_Title_case_of_Gender()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="maLE", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
                new Person { Age = 20, Name="Raj", Gender="feMaLe", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>Test for Title case of Pet name</summary>
        [TestMethod]
        public void Test_for_Title_case_of_Pet_name()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="male", Pets = new List<Pet> { new Pet { Name="oscar wild", Species = "Cat"} } },
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].PetNames[0] == "Oscar Wild");
        }

        /// <summary>Test for Gender sequence where male should be first</summary>
        [TestMethod]
        public void Test_for_Gender_sequence_where_male_should_be_first()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="male", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
                new Person { Age = 20, Name="Raj", Gender="Female", Pets = new List<Pet> { new Pet { Name="Oscar1", Species = "Cat"} } },
                new Person { Age = 10, Name="Michael", Gender="Male", Pets = new List<Pet> { new Pet { Name="Oscar2", Species = "Cat"} } },

            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>Result list should have two items</summary>
        [TestMethod]
        public void Result_list_should_have_two_items()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="male", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
                new Person { Age = 20, Name="Raj", Gender="male", Pets = new List<Pet> { new Pet { Name="Oscar1", Species = "Cat"}, new Pet { Name = "Oscar4", Species = "Cat" } } },
                new Person { Age = 10, Name="Michael", Gender="Male", Pets = new List<Pet> { new Pet { Name="Oscar2", Species = "Cat"}, new Pet { Name = "Oscar4", Species = "Dog" } } },
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults.Count == 2);
        }

        /// <summary>Test for Petnames sort order</summary>
        [TestMethod]
        public void Test_for_Petnames_sort_order()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="male", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
                new Person { Age = 20, Name="Raj", Gender="male", Pets = new List<Pet> { new Pet { Name="Oscar1", Species = "Cat"}, new Pet { Name = "Oscar4", Species = "Cat" } } },
                new Person { Age = 10, Name="Michael", Gender="Male", Pets = new List<Pet> { new Pet { Name="Oscar2", Species = "Cat"}, new Pet { Name = "Oscar4", Species = "Dog" } } },
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>If only one Male person then return should have two records</summary>
        [TestMethod]
        public void If_only_one_Male_person_then_return_should_have_two_records()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Mital", Gender="Male", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>If only one female person then return should have two records</summary>
        [TestMethod]
        public void If_only_one_female_person_then_return_should_have_two_records()
        {
            var persons = new List<Person>
            {
                new Person { Age = 10, Name="Sian", Gender="Female", Pets = new List<Pet> { new Pet { Name="Oscar", Species = "Cat"} } },
            };
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>For No persons two items should be returned with no pets</summary>
        [TestMethod]
        public void For_No_persons_two_items_should_be_returned_with_no_pets()
        {
            var persons = new List<Person>();
            var petsResults = GetMockedService(persons).GetPetNamesGroupedUponOwnerGender("cat").Result;
            Assert.IsTrue(petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

    }
}
