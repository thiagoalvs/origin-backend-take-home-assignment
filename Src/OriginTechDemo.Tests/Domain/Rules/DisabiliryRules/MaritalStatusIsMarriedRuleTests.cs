using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreRules.DisabilityRules;
using System;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.DisabilityRules
{
    [TestClass]
    public class MaritalStatusIsMarriedRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnNegativeOneIfTheUserIsMarried()
        {
            UserInformation userInformation = new UserInformation()
            {
                MaritalStatus = EMaritalStatus.Married
            };

            var rule = new MaritalStatusIsMarriedRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == -1);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheUserIsSingle()
        {
            UserInformation userInformation = new UserInformation()
            {
                MaritalStatus = EMaritalStatus.Single
            };

            var rule = new MaritalStatusIsMarriedRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task ShouldReturnExceptionIfUserMaritalStatusIsNone()
        {
            UserInformation userInformation = new UserInformation()
            {
                MaritalStatus = EMaritalStatus.None
            };

            var rule = new MaritalStatusIsMarriedRule(_externalConfigurationService.Object);

            await rule.Validate(userInformation);
        }
    }
}
