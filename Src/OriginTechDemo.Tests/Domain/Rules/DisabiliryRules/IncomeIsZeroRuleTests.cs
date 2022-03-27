using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.DisabilityRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.DisabilityRules
{
    [TestClass]
    public class IncomeIsZeroRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNullIfUserIncomeIsZero()
        {
            UserInformation userInformation = new UserInformation()
            {
                Income = 0
            };

            var rule = new IncomeIsZeroRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public async Task ShouldReturnNullIfUserIncomeIsAboveZero()
        {
            UserInformation userInformation = new UserInformation()
            {
                Income = 100
            };

            var rule = new IncomeIsZeroRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
