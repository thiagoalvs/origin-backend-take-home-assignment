using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreCalculators
{
    public abstract class BaseScoreCalculator : ILifeScoreCalculator
    {
        private readonly EIsuranceLine _insuranceLine;
        private readonly IEnumerable<IScoreRule> _rules;
        private readonly ILoggingService _loggingService;

        public BaseScoreCalculator(EIsuranceLine insuranceLine, IEnumerable<IScoreRule> rules, ILoggingService loggingService)
        {
            _insuranceLine = insuranceLine;
            _rules = rules;
            _loggingService = loggingService;

        }

        public virtual async Task<EScore> Calculate(UserInformation userInformation)
        {
            bool isRuleActive = default;
            int? ruleScore = default;
            int? finalScore = default;

            finalScore = userInformation.RiskQuestions.Sum();

            _loggingService.Log($"InsuranceLine:{_insuranceLine} | InitialScore:{finalScore}(RiskQuestionsSum)");

            foreach (var rule in _rules.Where(x => x.InsuranceLine == _insuranceLine || x.InsuranceLine == EIsuranceLine.Shared))
            {
                isRuleActive = await rule.IsActive();
                if (isRuleActive)
                {
                    ruleScore = await rule.Validate(userInformation);

                    if (ruleScore == null)
                    {
                        finalScore = null;
                        _loggingService.Log($"InsuranceLine:{_insuranceLine} | Rule:{rule.Name} | IsActive:{isRuleActive} | RuleScore: {ruleScore} | CurrentScore:{finalScore}");
                        break;
                    }

                    finalScore += ruleScore;
                }

                _loggingService.Log($"InsuranceLine:{_insuranceLine} | Rule:{rule.Name} | IsActive:{isRuleActive} | RuleScore: {ruleScore} | CurrentScore:{finalScore}");
            }

            //Score consolidation
            var result = await ConsolidateScore(finalScore);

            _loggingService.Log($"InsuranceLine:{_insuranceLine} | FinalScore:{finalScore} | Result:{result}");

            return result;
        }

        public virtual async Task<EScore> ConsolidateScore(int? score)
        { 
            if (score == null)
                return EScore.Ineligible;

            if (score <= 0)
                return EScore.Economic;

            if (score == 1 || score == 2)
                return EScore.Regular;

            return EScore.Responsible;
        }
    }
}
