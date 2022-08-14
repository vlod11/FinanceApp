namespace Data.MongoDb.Exceptions
{
    public class FailRepositoryException: RepositoryException
    {
        public FailRepositoryException(string description) : base(ERepositoryCode.Fail, description)
        {
        }
    }
}