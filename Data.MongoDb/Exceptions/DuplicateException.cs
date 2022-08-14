namespace Data.MongoDb.Exceptions
{
    public class DuplicateException: RepositoryException
    {
        public DuplicateException() : base(ERepositoryCode.Duplicate, string.Empty)
        {
        }
    }
}