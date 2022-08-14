using Core.FinantialResultAggregate;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Helpers
{
    /// <summary>
    /// Class created for test purposes. Delete in production
    /// </summary>
    public class SeedDatabase
    {
        protected readonly IMongoDatabase _mongoDatabase;

        public SeedDatabase(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }
        public void Seed()
        {
            var collection = _mongoDatabase.GetCollection<FinantialResult>(typeof(FinantialResult).Name);

            collection.DeleteMany(x => true);

            var financialResults = new List<FinantialResult>()
            { new FinantialResult()
                {
                    Year = 2022,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 25550M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Libility
                },
                new FinantialResult()
                {
                    Year = 2013,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 10M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Libility
                },
                new FinantialResult()
                {
                    Year = 2013,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 20M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Transport
                },
                new FinantialResult()
                {
                    Year = 2015,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 30M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Libility
                },
                new FinantialResult()
                {
                    Year = 2022,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 20M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Transport
                },
                new FinantialResult()
                {
                    Year = 2008,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 10M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Libility
                },
                new FinantialResult()
                {
                    Year = 2022,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 13230M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Libility
                },
                new FinantialResult()
                {
                    Year = 2022,
                    Country = Common.Enums.ECountry.CzechRepublic,
                    GrowthWrittenPremium = 234763.43M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Housing
                },
                new FinantialResult()
                {
                    Year = 2013,
                    Country = Common.Enums.ECountry.UK,
                    GrowthWrittenPremium = 30M,
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Housing
                }
            };

            collection.InsertMany(financialResults);
        }
    }
}
