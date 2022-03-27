using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Collections.Generic;

namespace OriginTechDemo.Domain.ScoreCalculators
{
    public class LifeScoreCalculator : BaseScoreCalculator, ILifeScoreCalculator
    {
        public LifeScoreCalculator(IEnumerable<IScoreRule> rules, ILoggingService loggingService) : base(EIsuranceLine.Life, rules, loggingService)
        {
            
        }
    }
}
