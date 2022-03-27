using Microsoft.AspNetCore.Mvc;
using OriginTechDemo.Application.Interfaces.Services;
using OriginTechDemo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OriginTechDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RiskProfileController : Controller
    {
        private readonly IRiskProfileService _riskProfileService;

        public RiskProfileController(IRiskProfileService riskProfileService)
        {
            _riskProfileService = riskProfileService;
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromBody]UserInformationViewModel vm)
        {
            var result = await _riskProfileService.CalculateRiskProfile(vm);

            return new ObjectResult(result.Data)
            {
                StatusCode = (int)result.StatusCode
            };
        }
    }
}
