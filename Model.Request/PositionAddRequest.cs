using Requests.Attributes;
using System.Collections.Generic;
using Common.Enums;

namespace Requests
{
    public class GrossWrittenPremiumAverageRequest
    {
        [RequiredNotEmpty]
        public ECountry Country { get; set; }
        [RequiredNotEmpty]
        public List<ELineOfBusiness> LinesOfBusiness { get; set; }
    }
}