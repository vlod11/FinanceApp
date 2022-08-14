using AutoMapper;
using Core.FinantialResultAggregate;
using Data.MongoDb.Repositories;
using Data.MongoDb.Repositories.Interfaces;
using MongoDB.Driver;
using Moq;
using Services.Services;
using System.Linq.Expressions;

namespace EmployeesTest
{
    public class GrossWrittenPremiumRepositoryUnitTest
    {
        public Mock<IFinantialResultRepository> _repo = new Mock<IFinantialResultRepository>();

        [Fact]
        public async void GetAverageAsync_TestFinantialResultSPassed_CalculatedCorrectlly()
        {
            // Arrange
            var financialResults = new List<FinantialResult>()
            { 
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
                    LineOfBusiness = Common.Enums.ELineOfBusiness.Libility
                },
            }.AsEnumerable<FinantialResult>();

            _repo.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<FinantialResult, bool>>>()))
                .Returns(Task.FromResult(financialResults));

            // Act
            var service = new GrossWrittenPremiumService(_repo.Object);
            var resviceResult =await  service.GetAverageAsync(new Requests.GrossWrittenPremiumAverageRequest()
            {
                Country = Common.Enums.ECountry.CzechRepublic,
                LinesOfBusiness = new List<Common.Enums.ELineOfBusiness>()
                { Common.Enums.ELineOfBusiness.Libility}
            }, 2005, 2018);


            //Assert

            var dictionary = new Dictionary<string, decimal>();
            dictionary.Add("Libility", 15M);

            Assert.Equal(dictionary, resviceResult.Result);
        }
    }
}