using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.SharedRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.SharedRules
{
    [TestClass]
    public class AgeUnder30RuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNegativeTwoIfUserAgeIsLessThan30()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 29
            };

            var rule = new AgeUnder30Rule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == -2);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfUserAgeIsEqualOrAbove30()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 30
            };

            var rule = new AgeUnder30Rule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
