using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.VehicleRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.VehicleRules
{
    [TestClass]
    public class VehicleIsPartOfUsersBelongingsRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNullIfTheHouseIsNull()
        {
            UserInformation userInformation = new UserInformation()
            {
                Vehicle = null
            };

            var rule = new VehicleIsPartOfUsersBelongingsRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheHouseIsNotNull()
        {
            UserInformation userInformation = new UserInformation()
            {
                Vehicle = new VehicleInformation()
            };

            var rule = new VehicleIsPartOfUsersBelongingsRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
