using System.Collections.Generic;
using System.Threading.Tasks;
using Requests;

namespace Services.Interfaces
{
    public interface IGrossWrittenPremiumService
    {
        Task<ServiceResult<Dictionary<string, decimal>>> GetAverageAsync(GrossWrittenPremiumAverageRequest gwpRequest, int yearFromIncluded, int yearToIncluded);
    }
}