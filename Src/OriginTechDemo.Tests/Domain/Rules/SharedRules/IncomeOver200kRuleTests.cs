using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.SharedRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.SharedRules
{
    [TestClass]
    public class IncomeOver200kRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNegativeOneIfUserIncomeIsAbove200K()
        {
            UserInformation userInformation = new UserInformation()
            {
                Income = 201_000
            };

            var rule = new IncomeOver200kRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == -1);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfUserIncomeIsEqualOrLessThan200K()
        {
            UserInformation userInformation = new UserInformation()
            {
                Age = 200_000
            };

            var rule = new IncomeOver200kRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
