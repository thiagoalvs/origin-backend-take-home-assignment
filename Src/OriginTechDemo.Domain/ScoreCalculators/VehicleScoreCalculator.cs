using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Collections.Generic;

namespace OriginTechDemo.Domain.ScoreCalculators
{
    public class VehicleScoreCalculator : BaseScoreCalculator, IVehicleScoreCalculator
    {
        public VehicleScoreCalculator(IEnumerable<IScoreRule> rules, ILoggingService loggingService) : base(EIsuranceLine.Vehicle, rules, loggingService)
        {
            
        }
    }
}
