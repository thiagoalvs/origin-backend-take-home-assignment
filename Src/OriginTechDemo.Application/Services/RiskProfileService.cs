using AutoMapper;
using OriginTechDemo.Application.Interfaces.Services;
using OriginTechDemo.Application.Models;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Application.Services
{
    public class RiskProfileService : IRiskProfileService
    {
        private readonly IMapper _mapper;
        private readonly ILifeScoreCalculator _lifeScoreCalculator;
        private readonly IDisabilityScoreCalculator _disabilityScoreCalculator;
        private readonly IHouseScoreCalculator _houseScoreCalculator;
        private readonly IVehicleScoreCalculator _vehicleScoreCalculator;

        public RiskProfileService(
            IMapper mapper,
            ILifeScoreCalculator lifeScoreCalculator,
            IDisabilityScoreCalculator disabilityScoreCalculator,
            IHouseScoreCalculator houseScoreCalculator,
            IVehicleScoreCalculator vehicleScoreCalculator)
        {
            _mapper = mapper;

            _lifeScoreCalculator = lifeScoreCalculator;
            _disabilityScoreCalculator = disabilityScoreCalculator;
            _houseScoreCalculator = houseScoreCalculator;
            _vehicleScoreCalculator = vehicleScoreCalculator;
        }

        public async Task<GenericResult> CalculateRiskProfile(UserInformationViewModel vm)
        {
            if (vm == null)
                return new GenericResult(HttpStatusCode.BadRequest, "No valid data provided");

            var validationResult = await vm.ValidateAsync();

            if (!validationResult.IsValid)
                return new GenericResult(HttpStatusCode.BadRequest, validationResult.Errors);

            var userInformation = _mapper.Map<UserInformation>(vm);

            var lifeScore = await _lifeScoreCalculator.Calculate(userInformation);
            var disabilityScore = await _disabilityScoreCalculator.Calculate(userInformation);
            var houseScore = await _houseScoreCalculator.Calculate(userInformation);
            var vehicleScore = await _vehicleScoreCalculator.Calculate(userInformation);

            var result = new RiskProfileViewModel
                (
                    vehicleScore.ToString().ToLower(),
                    disabilityScore.ToString().ToLower(),
                    houseScore.ToString().ToLower(),
                    lifeScore.ToString().ToLower()
                );

            return new GenericResult(HttpStatusCode.OK, result);
        }
    }
}
