using Microsoft.VisualStudio.TestTools.UnitTesting;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Validators;
using System;
using System.Collections.Generic;

namespace OriginTechDemo.Tests.Application.Validators
{
    [TestClass]
    public class UserInformationViewModelValidatorTests
    {
        [TestMethod]
        public void ShouldReturnSuccess()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OwNeD" },
                income = 100_000,
                marital_status = "SINGLE",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWithoutHouseAndVehicle()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = null,
                income = 100_000,
                marital_status = "single",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWithoutVehicle()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OWNED" },
                income = 100_000,
                marital_status = "SiNgLe",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWithoutHouse()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = null,
                income = 100_000,
                marital_status = "SINGLE",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorIfUserAgeIsLessThanZero()
        {
            var vm = new UserInformationViewModel()
            {
                age = -1,
                dependents = 0,
                house = null,
                income = 100_000,
                marital_status = "SINGLE",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnErrorIfUserDependentsNumberIsLessThanZero()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = -1,
                house = null,
                income = 100_000,
                marital_status = "SINGLE",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnErrorIfUserIncomeIsLessThanZero()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = null,
                income = -1,
                marital_status = "SINGLE",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnErrorIfUsersMaritalStatusProvidedIsNotValid()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = null,
                income = 100_000,
                marital_status = "InVaLiD",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnErrorIfUsersRiskQuestionsArrayIsNull()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = null,
                income = 100_000,
                marital_status = "single",
                risk_questions = null,
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnErrorIfUsersRiskQuestionsArrayCountIsNotThree()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = null,
                income = 100_000,
                marital_status = "single",
                risk_questions = new List<int> { 1 },
                vehicle = null
            };

            var validator = new UserInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }
    }
}
