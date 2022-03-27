using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.VehicleRules;
using System;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.VehicleRules
{
    [TestClass]
    public class VehicleIsAtMostFiveYearsOldRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnOneIfTheVehicleIsAtMostFiveYearsOld()
        {
            UserInformation userInformation = new UserInformation()
            {
                Vehicle = new VehicleInformation { Year = DateTime.Now.AddYears(-5).Year }
            };

            var rule = new VehicleIsAtLeastFiveYearsOldRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheVehicleIsOlderThanFiveYearsOld()
        {
            UserInformation userInformation = new UserInformation()
            {
                Vehicle = new VehicleInformation { Year = DateTime.Now.AddYears(-6).Year }
            };

            var rule = new VehicleIsAtLeastFiveYearsOldRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheVehicleIsNull()
        {
            UserInformation userInformation = new UserInformation()
            {
                Vehicle = null
            };

            var rule = new VehicleIsAtLeastFiveYearsOldRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
