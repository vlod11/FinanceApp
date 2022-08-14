using Core.FinantialResultAggregate;
using Data.MongoDb.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Data.MongoDb.Repositories
{
    public class FinantialResultRepository : BaseMongoDbRepository<FinantialResult>, IFinantialResultRepository
    {
        public FinantialResultRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}