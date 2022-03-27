using Microsoft.VisualStudio.TestTools.UnitTesting;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Validators;
using System;

namespace OriginTechDemo.Tests.Application.Validators
{
    [TestClass]
    public class VehicleInformationViewModelValidatorTests
    {
        [TestMethod]
        public void ShouldReturnErrorIfVehicleYearIsLessThan1885()
        {
            var vm = new VehicleInformationViewModel()
            {
                year = 1884
            };

            var validator = new VehicleInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnErrorIfVehicleYearIsGreaterThanTwoYearsInTheFuture()
        {
            var vm = new VehicleInformationViewModel()
            {
                year = DateTime.Now.AddYears(2).Year
            };

            var validator = new VehicleInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(!result.IsValid && result.Errors.Count == 1);
        }

        [TestMethod]
        public void ShouldReturnSuccess()
        {
            var vm = new VehicleInformationViewModel()
            {
                year = DateTime.Now.Year
            };

            var validator = new VehicleInformationViewModelValidator();

            var result = validator.Validate(vm);

            Assert.IsTrue(result.IsValid);
        }

    }
}
