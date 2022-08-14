using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Requests;
using Services.Interfaces;
using Web.Helpers;

namespace Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("/server/api/gwp")]
    [ApiController]
    public class GrossWrittenPremiumController : ControllerBase
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IDateService _dateService;
        private readonly IGrossWrittenPremiumService _gwpService;

        const int YearFromIncluded = 2008;
        const int YearToIncluded = 2015;


        public GrossWrittenPremiumController(IServiceResultMapper mapper, IDateService dateService, IGrossWrittenPremiumService gwpService)
        {
            _viewMapper = mapper;
            _dateService = dateService;
            _gwpService = gwpService;
        }

        [HttpPost("avg")]
        public async Task<ActionResult<Dictionary<string, decimal>>> GetGrossWrittenPremiumAverageAsync([FromBody] GrossWrittenPremiumAverageRequest request)
        {
            var result = await _gwpService.GetAverageAsync(request, YearFromIncluded, YearToIncluded);
            return _viewMapper.ServiceResultToContentResult(result);
        }
    }
}