namespace Data.MongoDb.Exceptions
{
    public class MissingEntityException: RepositoryException
    {
        public MissingEntityException(string entityId) : base(ERepositoryCode.MissingEntity, $"Missing entity {entityId}")
        {
        }
    }
}