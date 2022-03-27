using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreCalculators;
using OriginTechDemo.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.Calculators
{
    [TestClass]
    public class VehicleScoreCalculatorTests
    {

        private IEnumerable<IScoreRule> _rules;
        private Mock<ILoggingService> _loggingService;

        public VehicleScoreCalculatorTests()
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
                Dependents = 0,
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Owned },
                Income = 100_000,
                MaritalStatus = EMaritalStatus.Married,
                RiskQuestions = new List<int> { 0, 1, 0 }, //+1
                Vehicle = new VehicleInformation() { Year = DateTime.Now.AddYears(-5).Year } //+1
            };

            //ExpectedScore: 0

            var calculator = new VehicleScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Economic);
        }

        [TestMethod]
        public async Task ShouldReturnRegular()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 31, //-1
                Dependents = 0,
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Mortgaged },
                Income = 100_000,
                MaritalStatus = EMaritalStatus.Married,
                RiskQuestions = new List<int> { 0, 1, 0 }, //+2
                Vehicle = new VehicleInformation() { Year = DateTime.Now.AddYears(-5).Year } //+1
            };

            //ExpectedScore: 1

            var calculator = new VehicleScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Regular);
        }

        [TestMethod]
        public async Task ShouldReturnResponsible()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 41,
                Dependents = 0,
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.Mortgaged },
                Income = 100_000,
                MaritalStatus = EMaritalStatus.Married,
                RiskQuestions = new List<int> { 0, 1, 1 }, //+2
                Vehicle = new VehicleInformation() { Year = DateTime.Now.AddYears(-5).Year } //+1
            };

            //ExpectedScore: 3

            var calculator = new VehicleScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Responsible);
        }

        [TestMethod]
        public async Task ShouldReturnIneligible()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 50, 
                Dependents = 2,
                House = null,
                Income = 0,
                MaritalStatus = EMaritalStatus.Married,
                RiskQuestions = new List<int> { 0, 1, 0 },
                Vehicle = null, //Ineligible
            };

            var calculator = new VehicleScoreCalculator(_rules, _loggingService.Object);

            var result = await calculator.Calculate(userInformation);

            Assert.IsTrue(result == EScore.Ineligible);
        }
    }
}
