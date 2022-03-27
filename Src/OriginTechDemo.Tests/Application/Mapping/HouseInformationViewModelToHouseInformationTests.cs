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
    public class HouseInformationViewModelToHouseInformationTests
    {
        private readonly IMapper _mapper;

        public HouseInformationViewModelToHouseInformationTests()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new HouseInformationViewModelToHouseInformationProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [TestMethod]
        public void ShouldSuccessfullyMapWhenOwnershipStatusIsOwned()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "OwNeD"
            };

            var result = _mapper.Map<HouseInformation>(vm);

            Assert.IsTrue(result.OwnershipStatus == EOwnershipStatus.Owned);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapWhenOwnershipStatusIsMortgaged()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "MORTgaged"
            };

            var result = _mapper.Map<HouseInformation>(vm);

            Assert.IsTrue(result.OwnershipStatus == EOwnershipStatus.Mortgaged);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapWhenOwnershipStatusIsNone()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "NoNe"
            };

            var result = _mapper.Map<HouseInformation>(vm);

            Assert.IsTrue(result.OwnershipStatus == EOwnershipStatus.None);
        }

        [TestMethod]
        public void ShouldSuccessfullyMapToNoneWhenOwnershipStatusIsNotValid()
        {
            var vm = new HouseInformationViewModel()
            {
                ownership_status = "Invalid"
            };

            var result = _mapper.Map<HouseInformation>(vm);

            Assert.IsTrue(result.OwnershipStatus == EOwnershipStatus.None);
        }
    }
}
