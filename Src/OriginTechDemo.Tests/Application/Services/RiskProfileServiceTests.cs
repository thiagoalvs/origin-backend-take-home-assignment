using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OriginTechDemo.Application.Services;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreCalculators;
using OriginTechDemo.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Rules.Domain.Calculators
{
    [TestClass]
    public class RiskProfileServiceTests
    {
        private Mock<IMapper> _mapper;

        private Mock<ILifeScoreCalculator> _lifeScoreCalculator;
        private Mock<IDisabilityScoreCalculator> _disabilityScoreCalculator;
        private Mock<IHouseScoreCalculator> _houseScoreCalculator;
        private Mock<IVehicleScoreCalculator> _vehicleScoreCalculator;

        [TestInitialize]
        public void Initilize()
        {
            _mapper = new Mock<IMapper>();

            _lifeScoreCalculator = new Mock<ILifeScoreCalculator>();
            _disabilityScoreCalculator = new Mock<IDisabilityScoreCalculator>();
            _houseScoreCalculator = new Mock<IHouseScoreCalculator>();
            _vehicleScoreCalculator = new Mock<IVehicleScoreCalculator>();
        }

        [TestMethod]
        public async Task ShouldReturnSuccess()
        {
            var vm = new UserInformationViewModel()
            {
                age = 35,
                dependents = 2,
                house = new HouseInformationViewModel() { ownership_status = "owned" },
                income = 0,
                marital_status = "married",
                risk_questions = new List<int> { 0, 1, 0 },
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.AddYears(-4).Year }
            };

            _lifeScoreCalculator.Setup(method => method.Calculate(It.IsAny<UserInformation>())).ReturnsAsync(EScore.Regular);
            _disabilityScoreCalculator.Setup(method => method.Calculate(It.IsAny<UserInformation>())).ReturnsAsync(EScore.Ineligible);
            _houseScoreCalculator.Setup(method => method.Calculate(It.IsAny<UserInformation>())).ReturnsAsync(EScore.Economic);
            _vehicleScoreCalculator.Setup(method => method.Calculate(It.IsAny<UserInformation>())).ReturnsAsync(EScore.Regular);

            var service = new RiskProfileService(_mapper.Object,
                                                        _lifeScoreCalculator.Object,
                                                        _disabilityScoreCalculator.Object,
                                                        _houseScoreCalculator.Object,
                                                        _vehicleScoreCalculator.Object);

            var result = await service.CalculateRiskProfile(vm);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue((result.Data as RiskProfileViewModel).auto == "regular");
            Assert.IsTrue((result.Data as RiskProfileViewModel).disability == "ineligible");
            Assert.IsTrue((result.Data as RiskProfileViewModel).home == "economic");
            Assert.IsTrue((result.Data as RiskProfileViewModel).life == "regular");
        }

        [TestMethod]
        public async Task ShouldReturnFailureIfViewModelIsNull()
        {
            UserInformationViewModel vm = null;

            var service = new RiskProfileService(_mapper.Object,
                                                        _lifeScoreCalculator.Object,
                                                        _disabilityScoreCalculator.Object,
                                                        _houseScoreCalculator.Object,
                                                        _vehicleScoreCalculator.Object);

            var result = await service.CalculateRiskProfile(vm);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            Assert.IsTrue((result.Data as List<string>).Count == 1);
        }

        [TestMethod]
        public async Task ShouldReturnFailureIfViewModelIsInvalid()
        {
            var vm = new UserInformationViewModel()
            {
                age = 35,
                dependents = 2,
                house = new HouseInformationViewModel() { ownership_status = "owned" },
                income = 0,
                marital_status = "married",
                risk_questions = null,
                vehicle = new VehicleInformationViewModel() { year = DateTime.Now.AddYears(-4).Year }
            };

            var service = new RiskProfileService(_mapper.Object,
                                                        _lifeScoreCalculator.Object,
                                                        _disabilityScoreCalculator.Object,
                                                        _houseScoreCalculator.Object,
                                                        _vehicleScoreCalculator.Object);

            var result = await service.CalculateRiskProfile(vm);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            Assert.IsTrue((result.Data as List<string>).Count == 1);
        }
    }
}
