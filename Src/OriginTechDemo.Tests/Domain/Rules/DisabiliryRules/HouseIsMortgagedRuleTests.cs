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
    public class HouseIsMortgagedRuleTests
    {
        private Mock<IExternalConfigurationService> _externalConfigurationService;

        [TestInitialize]
        public void Initilize()
        {
            _externalConfigurationService = new Mock<IExternalConfigurationService>();
        }

        [TestMethod]
        public async Task ShouldReturnOneIfTheHouseIsMortgaged()
        {
            UserInformation userInformation = new UserInformation()
            {
                House = new HouseInformation()
                {
                    OwnershipStatus = EOwnershipStatus.Mortgaged
                }
            };

            var rule = new HouseIsMortgagedRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheHouseIsOwned()
        {
            UserInformation userInformation = new UserInformation()
            {
                House = new HouseInformation()
                {
                    OwnershipStatus = EOwnershipStatus.Owned
                }
            };

            var rule = new HouseIsMortgagedRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task ShouldReturnExceptionIfHouseOwnershipStatusIsNone()
        {
            UserInformation userInformation = new UserInformation()
            {
                House = new HouseInformation() { OwnershipStatus = EOwnershipStatus.None }
            };

            var rule = new HouseIsMortgagedRule(_externalConfigurationService.Object);

            await rule.Validate(userInformation);
        }

        [TestMethod]
        public async Task ShouldReturnZeroIfTheHouseIsNull()
        {
            UserInformation userInformation = new UserInformation()
            {
                House = null
            };

            var rule = new HouseIsMortgagedRule(_externalConfigurationService.Object);

            var result = await rule.Validate(userInformation);

            Assert.IsTrue(result == 0);
        }
    }
}
