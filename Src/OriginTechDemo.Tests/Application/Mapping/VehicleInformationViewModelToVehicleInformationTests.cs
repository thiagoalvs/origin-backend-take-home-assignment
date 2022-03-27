using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OriginTechDemo.Application.Mapping;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Application.Mapping
{
    [TestClass]
    public class VehicleInformationViewModelToVehicleInformationTests
    {
        private readonly IMapper _mapper;

        public VehicleInformationViewModelToVehicleInformationTests()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new VehicleInformationViewModelToVehicleInformationProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [TestMethod]
        public void ShouldSuccessfullyMapVehicleYear()
        {
            var vm = new VehicleInformation()
            {
                Year = DateTime.Now.Year
            };

            var result = _mapper.Map<VehicleInformation>(vm);

            Assert.IsTrue(result.Year == vm.Year);
        }
    }
}
