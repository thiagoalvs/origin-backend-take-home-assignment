using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OriginTechDemo.Application.Mapping;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Application.Mapping
{
    [TestClass]
    public class UserInformationViewModelToUserInformationTests
    {
        private readonly IMapper _mapper;

        public UserInformationViewModelToUserInformationTests()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new HouseInformationViewModelToHouseInformationProfile());
                config.AddProfile(new VehicleInformationViewModelToVehicleInformationProfile());
                config.AddProfile(new UserInformationViewModelToUserInformationProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [TestMethod]
        public void ShouldSuccessfullyMapUser()
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

            var result = _mapper.Map<UserInformation>(vm);

            Assert.IsTrue(result.Age == vm.age);
            Assert.IsTrue(result.Dependents == vm.dependents);
            Assert.IsTrue(result.House.OwnershipStatus == EOwnershipStatus.Owned);
            Assert.IsTrue(result.Income == vm.income);
            Assert.IsTrue(result.MaritalStatus == EMaritalStatus.Single);
            Assert.IsTrue(result.RiskQuestions.Count == 3);
            Assert.IsTrue(result.RiskQuestions[0] == 1);
            Assert.IsTrue(result.RiskQuestions[1] == 0);
            Assert.IsTrue(result.RiskQuestions[2] == 0);
            Assert.IsTrue(result.Vehicle.Year == vm.vehicle.year);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapUserIfRisksQuestionsAreNull()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OwNeD" },
                income = 100_000,
                marital_status = "SINGLE",
                risk_questions = null,
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var result = _mapper.Map<UserInformation>(vm);

            Assert.IsTrue(result.Age == vm.age);
            Assert.IsTrue(result.Dependents == vm.dependents);
            Assert.IsTrue(result.House.OwnershipStatus == EOwnershipStatus.Owned);
            Assert.IsTrue(result.Income == vm.income);
            Assert.IsTrue(result.MaritalStatus == EMaritalStatus.Single);
            Assert.IsTrue(result.RiskQuestions.Count == 0);
            Assert.IsTrue(result.Vehicle.Year == vm.vehicle.year);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapUserWithSingleMaritalStatus()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OwNeD" },
                income = 100_000,
                marital_status = "sinGle",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var result = _mapper.Map<UserInformation>(vm);

            Assert.IsTrue(result.Age == vm.age);
            Assert.IsTrue(result.Dependents == vm.dependents);
            Assert.IsTrue(result.House.OwnershipStatus == EOwnershipStatus.Owned);
            Assert.IsTrue(result.Income == vm.income);
            Assert.IsTrue(result.MaritalStatus == EMaritalStatus.Single);
            Assert.IsTrue(result.RiskQuestions.Count == 3);
            Assert.IsTrue(result.RiskQuestions[0] == 1);
            Assert.IsTrue(result.RiskQuestions[1] == 0);
            Assert.IsTrue(result.RiskQuestions[2] == 0);
            Assert.IsTrue(result.Vehicle.Year == vm.vehicle.year);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapUserWithMarriedMaritalStatus()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OwNeD" },
                income = 100_000,
                marital_status = "MarrieD",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var result = _mapper.Map<UserInformation>(vm);

            Assert.IsTrue(result.Age == vm.age);
            Assert.IsTrue(result.Dependents == vm.dependents);
            Assert.IsTrue(result.House.OwnershipStatus == EOwnershipStatus.Owned);
            Assert.IsTrue(result.Income == vm.income);
            Assert.IsTrue(result.MaritalStatus == EMaritalStatus.Married);
            Assert.IsTrue(result.RiskQuestions.Count == 3);
            Assert.IsTrue(result.RiskQuestions[0] == 1);
            Assert.IsTrue(result.RiskQuestions[1] == 0);
            Assert.IsTrue(result.RiskQuestions[2] == 0);
            Assert.IsTrue(result.Vehicle.Year == vm.vehicle.year);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapUserWithoutMaritalStatus()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OwNeD" },
                income = 100_000,
                marital_status = "nOnE",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var result = _mapper.Map<UserInformation>(vm);

            Assert.IsTrue(result.Age == vm.age);
            Assert.IsTrue(result.Dependents == vm.dependents);
            Assert.IsTrue(result.House.OwnershipStatus == EOwnershipStatus.Owned);
            Assert.IsTrue(result.Income == vm.income);
            Assert.IsTrue(result.MaritalStatus == EMaritalStatus.None);
            Assert.IsTrue(result.RiskQuestions.Count == 3);
            Assert.IsTrue(result.RiskQuestions[0] == 1);
            Assert.IsTrue(result.RiskQuestions[1] == 0);
            Assert.IsTrue(result.RiskQuestions[2] == 0);
            Assert.IsTrue(result.Vehicle.Year == vm.vehicle.year);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapUserWithInvalidMaritalStatus()
        {
            var vm = new UserInformationViewModel()
            {
                age = 25,
                dependents = 0,
                house = new HouseInformationViewModel() { ownership_status = "OwNeD" },
                income = 100_000,
                marital_status = "invalid",
                risk_questions = new List<int> { 1, 0, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.Year }
            };

            var result = _mapper.Map<UserInformation>(vm);

            Assert.IsTrue(result.Age == vm.age);
            Assert.IsTrue(result.Dependents == vm.dependents);
            Assert.IsTrue(result.House.OwnershipStatus == EOwnershipStatus.Owned);
            Assert.IsTrue(result.Income == vm.income);
            Assert.IsTrue(result.MaritalStatus == EMaritalStatus.None);
            Assert.IsTrue(result.RiskQuestions.Count == 3);
            Assert.IsTrue(result.RiskQuestions[0] == 1);
            Assert.IsTrue(result.RiskQuestions[1] == 0);
            Assert.IsTrue(result.RiskQuestions[2] == 0);
            Assert.IsTrue(result.Vehicle.Year == vm.vehicle.year);
        }
    }
}
