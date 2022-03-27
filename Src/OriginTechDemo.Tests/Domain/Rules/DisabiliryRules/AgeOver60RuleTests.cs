using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.DisabilityRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.DisabilityRules
{
    [TestClass]
    public class AgeOver60RuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNullIfUserIsOver60YearsOld()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 61
            };

            var rule = new AgeOver60Rule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfUserIsUnder60YearsOld()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 59
            };

            var rule = new AgeOver60Rule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
