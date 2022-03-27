using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Tests.Helpers
{
    public class RulesHelper
    {
        private static IEnumerable<IScoreRule> rules;
        public static IEnumerable<IScoreRule> Rules
        {
            get
            {
                if (rules == null)
                    rules = GetAllRules();

                return rules;
            }
        }

        private static IEnumerable<IScoreRule> GetAllRules()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();

            var rulesSet = webHost.Services.GetServices<IScoreRule>();

            var externalConfigurationService = new Mock<IExternalConfigurationService>();

            externalConfigurationService.Setup(method => method.IsRuleActive(It.IsAny<string>())).ReturnsAsync(true);

            foreach (var rule in rulesSet)
                rule.SetExternalConfigurationService(externalConfigurationService.Object);

            return rulesSet;
        }
    }
}
