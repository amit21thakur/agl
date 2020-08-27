using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonPets.API.ApiClients.Interfaces;
using PersonPets.API.Controllers;
using PersonPets.API.Models;
using PersonPets.API.Services;
using PersonPets.Test.TestDataHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonPets.API;

namespace PersonPets.Test
{
    [TestClass]
    public class RequirementsTest
    {
        [TestInitialize]
        public void Init()
        {
            _loggerMock = new Mock<ILogger<PersonPetsController>>();
            _personListFactory = new PersonListFactory();
            _petsResultListFactory = new PetsResultListFactory();
        }


        #region private Variables and Methods

        private Mock<ILogger<PersonPetsController>> _loggerMock;

        private PersonListFactory _personListFactory;

        private PetsResultListFactory _petsResultListFactory;

        private List<PetsResult> GetResultsForPersonListType(PersonListType listType)
        {
            List<PetsResult> petsResults = null;
            var result = (OkObjectResult)GetResultForPersonPets(listType);
            if (result != null)
                petsResults = (List<PetsResult>)result.Value;
            return petsResults;
        }
        private IActionResult GetResultForPersonPets(PersonListType listType)
        {
            var persons = _personListFactory.GetPersonList(listType);

            var peopleApiClientMock = new Mock<IPeopleApiClient>();
            peopleApiClientMock.Setup(apiClient => apiClient.GetPersonData()).Returns(Task.FromResult(persons));

            var peopleServiceLoggerMock = new Mock<ILogger<PeopleService>>();

            var peopleService = new PeopleService(peopleApiClientMock.Object, new ValidatorService(), peopleServiceLoggerMock.Object);
            var controller = new PersonPetsController(_loggerMock.Object, peopleService);

            return controller.GetPetsData(string.Empty, "cat").Result;
        }
        private bool CompareListItemWise(List<PetsResult> source, List<PetsResult> target)
        {
            if (source == null && target == null)
                return true;

            if (source == null || target == null)
                return false;

            bool areSame = true;
            if (source.Count != target.Count)
                return false;
            for (var i = 0; i < source.Count; i++)
            {
                areSame = source[i].Equals(target[i]);
                if (!areSame)
                    break;
            }
            return areSame;
        }
        #endregion


        /// <summary>
        /// Req 01: When data is correct the output should have sorted list of cat names for each gender
        /// </summary>
        [TestMethod]
        public void When_data_is_correct_the_output_should_have_sorted_list_of_cat_names_for_each_gender()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.AllGoodData);
            var expectedResults = _petsResultListFactory.GetPetsResultList(PersonListType.AllGoodData);
            Assert.IsTrue(CompareListItemWise(petsResults, expectedResults));
        }

        /// <summary>
        /// Req 02: When data is correct the output should contain atleast two items one for each gender
        /// </summary>
        [TestMethod]
        public void When_data_is_correct_the_output_should_contain_atleast_two_items_one_for_each_gender()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.AllGoodData);
            Assert.IsTrue(petsResults != null && petsResults.Count == 2);
        }

        /// <summary>
        /// Req 03: When input data is correct the first item should be of male and second one for female.
        /// </summary>
        [TestMethod]
        public void When_input_data_is_correct_the_first_item_should_be_of_male_and_second_one_for_female()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.AllGoodData);
            Assert.IsTrue(petsResults != null && petsResults[0].OwnerGender == Constants.Male &&
                          petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>
        ///Req 04: When there is no data present then the list of pet names should be empty for each gender
        /// </summary>
        [TestMethod]
        public void When_there_is_no_data_present_then_the_list_of_pet_names_should_be_empty_for_each_gender()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.EmptyList);
            Assert.AreEqual(petsResults.Sum(x => x.PetNames.Count), 0);
        }

        /// <summary>
        ///Req 05: The total number of cats listed should match the expected count
        /// </summary>
        [TestMethod]
        public void The_total_number_of_cats_listed_should_match_the_expected_count()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.AllGoodData);
            Assert.AreEqual(petsResults.Sum(x => x.PetNames.Count), 7);
        }

        /// <summary>
        ///Req 06: The gender name and pet names should be in title case, irrespective of the input case
        /// </summary>
        [TestMethod]
        public void The_gender_name_and_pet_names_should_be_in_title_case_irrespective_of_the_input_case()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.IrregularFontCase);
            Assert.IsTrue(petsResults[0].OwnerGender == "Male" && petsResults[0].PetNames[0] == "Tina");
        }

        /// <summary>
        ///Req 07: Internal Server Error should be returned when any pet name is empty
        /// </summary>
        [TestMethod]
        public void Internal_Server_Error_should_be_returned_when_any_pet_name_is_empty()
        {
            var result = (StatusCodeResult)GetResultForPersonPets(PersonListType.MissingPetName);
            Assert.AreEqual(result.StatusCode, 500);
        }

        /// <summary>
        ///Req 08: Internal Server Error should be returned when any person age is lesser than one
        /// </summary>
        [TestMethod]
        public void Internal_Server_Error_should_be_returned_when_any_person_age_is_lesser_than_one()
        {
            var result = (StatusCodeResult)GetResultForPersonPets(PersonListType.AgeLessThanOne);
            Assert.AreEqual(result.StatusCode, 500);
        }

        /// <summary>
        ///Req 09: Internal Server Error should be returned if gender value is not correct
        /// </summary>
        [TestMethod]
        public void Internal_Server_Error_should_be_returned_if_gender_value_is_not_correct()
        {
            var result = (StatusCodeResult)GetResultForPersonPets(PersonListType.InvalidGender);
            Assert.AreEqual(result.StatusCode, 500);
        }

        /// <summary>
        ///Req 10: Internal Server Error should be returned if pet type is empty for any pet
        /// </summary>
        [TestMethod]
        public void Internal_Server_Error_should_be_returned_if_pet_type_is_empty_for_any_pet()
        {
            var result = (StatusCodeResult)GetResultForPersonPets(PersonListType.MissingPetType);
            Assert.AreEqual(result.StatusCode, 500);
        }

        /// <summary>
        ///Req 11: Duplicate pet names are shown multiple times in the output list for a given gender
        /// </summary>
        [TestMethod]
        public void Duplicate_pet_names_are_shown_multiple_times_in_the_output_list_for_a_given_gender()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.DuplicatePetNames);
            Assert.AreEqual(petsResults.Sum(x => x.PetNames.Count(name => name == "Timmy")), 3);
        }

        /// <summary>
        ///Req 12: Pet names with spaces should also get sorted properly
        /// </summary>
        [TestMethod]
        public void Pet_names_with_spaces_should_also_get_sorted_properly()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.PetNamesWithSpaces);
            var expectedResults = _petsResultListFactory.GetPetsResultList(PersonListType.PetNamesWithSpaces);
            Assert.IsTrue(CompareListItemWise(petsResults, expectedResults));
        }

        /// <summary>
        ///Req 13: The gender names should be grouped irrespective of the case of the gender names
        /// </summary>
        [TestMethod]
        public void The_gender_names_should_be_grouped_irrespective_of_the_case_of_the_gender_names()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.IrregularGenderCase);
            Assert.IsTrue(petsResults.Count == 2 && petsResults[0].OwnerGender == Constants.Male && petsResults[1].OwnerGender == Constants.Female);
        }

        /// <summary>
        ///Req 14: The persons with no pets should be ignored
        /// </summary>
        [TestMethod]
        public void The_persons_with_no_pets_should_be_ignored()
        {
            var petsResults = GetResultsForPersonListType(PersonListType.NoPets);
            Assert.AreEqual(petsResults.Sum(x => x.PetNames.Count), 1);
        }
    }
}
