using Microsoft.VisualStudio.TestTools.UnitTesting;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Validators;
using System;

namespace OriginTechDemo.Tests.Application.Validators
{
    [TestClass]
    public class HouseInformationViewModelValidatorTests
    {
        [TestMethod]
        public void ShouldReturnErrorIfHouseOwnershipStatusProvidedIsNotValid()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "invalid"
            };

            var validator = new HouseInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnSuccessIfHouseOwnershipStatusProvidedIsValidInLowerCase()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "owned"
            };

            var validator = new HouseInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessIfHouseOwnershipStatusProvidedIsValidInUpperCase()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "OWNED"
            };

            var validator = new HouseInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

    }
}
