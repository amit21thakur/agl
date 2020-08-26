using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonPets.API.Models;
using System;
using System.Collections.Generic;
using PersonPets.API;
using PersonPets.API.Services;

namespace PersonPets.UnitTest
{
    [TestClass]
    public class ValidatorUnitTest
    {

        private ValidatorService _validatorService;
        [TestInitialize]
        public void Init()
        {
            _validatorService = new ValidatorService();
        }

        /// <summary>
        /// When age is lesser than one then validation should fail
        /// </summary>
        [TestMethod]
        public void When_age_is_lesser_than_one_then_validation_should_fail()
        {
            var persons = new List<Person> { new Person { Age = 0, Gender = "Male", Name = "Amit", Pets = new List<Pet> { new Pet { Name = "Oscar", Species = "Cat" } } } };
            IList<string> errors;
            var isValid = _validatorService.Validate(persons, out errors);
            Assert.IsTrue(!isValid && errors != null && errors.Count == 1 && errors[0] == Constants.AgeGreaterThanZero);
        }


        /// <summary>
        /// When person name is empty then validation should fail
        /// </summary>
        [TestMethod]
        public void When_person_name_is_empty_then_validation_should_fail()
        {
            var persons = new List<Person> { new Person { Age = 20, Gender = "Male", Name = "", Pets = new List<Pet> { new Pet { Name = "Oscar", Species = "Cat" } } } };
            IList<string> errors;
            var isValid = _validatorService.Validate(persons, out errors);
            Assert.IsTrue(!isValid && errors != null && errors.Count == 1 && errors[0] == Constants.PersonNameEmpty);
        }

        /// <summary>
        /// When gender is not correct then validation should fail
        /// </summary>
        [TestMethod]
        public void When_gender_is_not_correct_then_validation_should_fail()
        {
            var persons = new List<Person> { new Person { Age = 20, Gender = "Males", Name = "Amit", Pets = new List<Pet> { new Pet { Name = "Oscar", Species = "Cat" } } } };
            IList<string> errors;
            var isValid = _validatorService.Validate(persons, out errors);
            Assert.IsTrue(!isValid && errors != null && errors.Count == 1 && errors[0] == Constants.InvalidGender);
        }

        /// <summary>
        /// When pet type is not defined than validation should fail
        /// </summary>
        [TestMethod]
        public void When_pet_type_is_not_defined_than_validation_should_fail()
        {
            var persons = new List<Person> { new Person { Age = 20, Gender = "Male", Name = "Amit", Pets = new List<Pet> { new Pet { Name = "Oscar", Species = "" } } } };
            IList<string> errors;
            var isValid = _validatorService.Validate(persons, out errors);
            Assert.IsTrue(!isValid && errors != null && errors.Count == 1 && errors[0] == Constants.EmptyPetType);
        }

        /// <summary>
        /// When pet name is not defined than validation should fail
        /// </summary>
        [TestMethod]
        public void When_pet_name_is_not_defined_than_validation_should_fail()
        {
            var persons = new List<Person> { new Person { Age = 20, Gender = "Male", Name = "Amit", Pets = new List<Pet> { new Pet { Name = null, Species = "Cat" } } } };
            IList<string> errors;
            var isValid = _validatorService.Validate(persons, out errors);
            Assert.IsTrue(!isValid && errors != null && errors.Count == 1 && errors[0] == Constants.EmptyPetName);
        }

        /// <summary>
        /// When person data is correct than validation should pass
        /// </summary>
        [TestMethod]
        public void When_person_data_is_correct_than_validation_should_pass()
        {
            var persons = new List<Person>
            {
                new Person { Age = 20, Gender = "Male", Name = "Amit", Pets = new List<Pet> { new Pet { Name = "Oscar", Species = "Cat" }, new Pet { Name = "Tiger", Species = "Dog" } } },
                new Person { Age = 20, Gender = "Female", Name = "Nav", Pets = new List<Pet> { new Pet { Name = "Oscar2", Species = "Cat" }, new Pet { Name = "Tiger", Species = "Dog" } } },
                new Person { Age = 20, Gender = "Male", Name = "John", Pets = new List<Pet> { new Pet { Name = "Oscar3", Species = "Cat" }, new Pet { Name = "Tiger", Species = "Dog" } } },
                new Person { Age = 20, Gender = "Female", Name = "Kathy", Pets = new List<Pet> { new Pet { Name = "Oscar4", Species = "Cat" }, new Pet { Name = "Tiger", Species = "Dog" } } },
            };
            IList<string> errors;
            var isValid = _validatorService.Validate(persons, out errors);
            Assert.IsTrue(isValid);
        }

    }
}
