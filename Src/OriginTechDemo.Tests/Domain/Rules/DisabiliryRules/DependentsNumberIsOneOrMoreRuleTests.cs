using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.DisabilityRules;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.DisabilityRules
{
    [TestClass]
    public class DependentsNumberIsOneOrMoreRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnOneIfTheUserHasOneOrMoreDependents()
        {
            UserInformation userInformation = new UserInformation()
            {
                Dependents = 1
            };

            var rule = new DependentsNumberIsOneOrMoreRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheUserHasNoDependents()
        {
            UserInformation userInformation = new UserInformation()
            {
                Dependents = 0
            };

            var rule = new DependentsNumberIsOneOrMoreRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
