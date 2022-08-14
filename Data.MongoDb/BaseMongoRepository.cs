using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Data.MongoDb.Exceptions;
using MongoDB.Driver;
using System.Linq.Expressions;
using System;

namespace Data.MongoDb
{
    public class BaseMongoDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : IDocumentEntity
    {
        protected const int DuplicateCode = 11000;
        protected readonly IMongoDatabase _mongoDatabase;

        public BaseMongoDbRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        protected string CollectionName => typeof(TEntity).Name;

        public async Task InsertAsync(TEntity entity)
        {
            try
            {
                await _mongoDatabase.GetCollection<TEntity>(CollectionName)
                                    .InsertOneAsync(entity);
            }
            catch (MongoWriteException ex)
            {
                if (ex.WriteError.Code == DuplicateCode)
                {
                    throw new DuplicateException();
                }

                throw new FailRepositoryException(ex.Message);
            }
            catch (MongoException ex)
            {
                throw new FailRepositoryException(ex.Message);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                var result = await _mongoDatabase.GetCollection<TEntity>(CollectionName)
                                                 .ReplaceOneAsync(x => x.Id == entity.Id, entity);

                if (result.MatchedCount != 1)
                {
                    throw new MissingEntityException(entity.Id);
                }
            }
            catch (MongoException ex)
            {
                throw new FailRepositoryException(ex.Message);
            }
        }

        public async Task<TEntity> GetByIdAsync(string id) =>
            await _mongoDatabase.GetCollection<TEntity>(CollectionName)
                                .Find(x => x.Id == id)
                                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _mongoDatabase.GetCollection<TEntity>(CollectionName)
                         .Find(predicate)
                         .ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _mongoDatabase.GetCollection<TEntity>(CollectionName)
                                .Find(x => true)
                                .ToListAsync();
    }
}