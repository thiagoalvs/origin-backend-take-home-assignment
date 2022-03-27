using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreCalculators;
using OriginTechDemo.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.Calculators
{
    [TestClass]
    public class LifeScoreCalculatorTests
    {

        private IEnumerable<IScoreRule> _rules;
        private Mock<ILoggingService> _loggingService;

        public LifeScoreCalculatorTests()
        {
            _rules = RulesHelper.Rules;
        }

        [TestInitialize]
        public void Initilize()
        {
            _loggingService = new Mock<ILoggingService>();
        }

        [TestMethod]
        public async Task ShouldReturnEconomic()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 25, //-2
                Dependents = 2, //+1
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Owned },
                Income = 300_000, //-1
                MaritalStatus = EMaritalStatus.Married, //+1
                RiskQuestions = new List<int> { 0, 1, 0 }, //+1
                Vehicle = new VehicleInformation() { Year = 2018 }
            };

            //ExpectedScore: 0

            var calculator = new LifeScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Economic);
        }

        [TestMethod]
        public async Task ShouldReturnRegular()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 35, //-1
                Dependents = 2, //+1
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Owned },
                Income = 0,
                MaritalStatus = EMaritalStatus.Married, //+1
                RiskQuestions = new List<int> { 0, 1, 0 }, //+1
                Vehicle = new VehicleInformation() { Year = 2018 }
            };

            //ExpectedScore: 2

            var calculator = new LifeScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Regular);
        }

        [TestMethod]
        public async Task ShouldReturnResponsible()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 41,
                Dependents = 2, //+1
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Owned },
                Income = 100_000,
                MaritalStatus = EMaritalStatus.Married, //+1
                RiskQuestions = new List<int> { 0, 1, 0 }, //+1
                Vehicle = new VehicleInformation() { Year = 2018 }
            };

            //ExpectedScore: 3

            var calculator = new LifeScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Responsible);
        }

        [TestMethod]
        public async Task ShouldReturnIneligible()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 61, //Ineligible
                Dependents = 2,
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Owned },
                Income = 0,
                MaritalStatus = EMaritalStatus.Married,
                RiskQuestions = new List<int> { 0, 1, 0 },
                Vehicle = new VehicleInformation() { Year = 2018 }
            };

            var calculator = new LifeScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Ineligible);
        }
    }
}
