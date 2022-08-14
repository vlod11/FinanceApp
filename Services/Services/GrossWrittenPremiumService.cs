using System.Collections.Generic;
using System.Threading.Tasks;
using Core.FinantialResultAggregate;
using Data.MongoDb.Repositories.Interfaces;
using Requests;
using Services.Interfaces;
using System.Linq;
using System;
using System.Runtime.ExceptionServices;

namespace Services.Services
{
    public class GrossWrittenPremiumService : IGrossWrittenPremiumService
    {
        private readonly IFinantialResultRepository _grossWrittenPremiumRepository;

        public GrossWrittenPremiumService(IFinantialResultRepository grossWrittenPremiumRepository)
        {
            _grossWrittenPremiumRepository = grossWrittenPremiumRepository;
        }

        public async Task<ServiceResult<Dictionary<string, decimal>>> GetAverageAsync(GrossWrittenPremiumAverageRequest gwpRequest, int yearFromIncluded, int yearToIncluded)
        {
            ServiceResult<Dictionary<string, decimal>> result = null;
            IEnumerable<FinantialResult> finantialResults = null;

            try
            {
                finantialResults = await _grossWrittenPremiumRepository
                    .GetByAsync(fr => fr.Country == gwpRequest.Country
                                    && gwpRequest.LinesOfBusiness.Contains(fr.LineOfBusiness)
                                    && fr.Year >= yearFromIncluded
                                    && fr.Year <= yearToIncluded);
            }
            catch (Exception ex)
            {
                //TODO: add logging
                ExceptionDispatchInfo.Capture(ex).Throw();
                result = ServiceResult<Dictionary<string, decimal>>.Fail(Core.EOperationResult.EntityNotFound);
            }

            if (result == null)
            {
                Dictionary<string, decimal> results = finantialResults.GroupBy(
                    p => p.LineOfBusiness,
                    p => p.GrowthWrittenPremium,
                    (key, g) => new { LineOfBusiness = key, Average = g.Average() })
                    .ToDictionary(x => x.LineOfBusiness.ToString(), y => y.Average);

                result = ServiceResult<Dictionary<string, decimal>>.Ok(results);
            }

            return result;
        }
    }
}