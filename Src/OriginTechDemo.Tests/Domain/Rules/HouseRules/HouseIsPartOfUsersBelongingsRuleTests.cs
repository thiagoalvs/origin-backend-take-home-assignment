using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.HouseRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.HouseRules
{
    [TestClass]
    public class HouseIsPartOfUsersBelongingsRuleTests
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
                House = null
            };

            var rule = new HouseIsPartOfUsersBelongingsRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheHouseIsNotNull()
        {
            UserInformation userInformation = new UserInformation()
            {
                House = new HouseInformation()
            };

            var rule = new HouseIsPartOfUsersBelongingsRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
