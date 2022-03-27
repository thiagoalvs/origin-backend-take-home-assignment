using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.SharedRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.SharedRules
{
    [TestClass]
    public class AgeBetween30and40RuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNegativeOneIfUserAgeIsBetween30And40YearsOld()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 35
            };

            var rule = new AgeBetween30and40Rule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == -1);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfUserAgeIsNotBetween30And40YearsOld()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 41
            };

            var rule = new AgeBetween30and40Rule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
