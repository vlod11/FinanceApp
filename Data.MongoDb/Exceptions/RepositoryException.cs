using Common;

namespace Data.MongoDb.Exceptions
{
    public class RepositoryException : ContentException
    {
        private const string DomainName = "Repository";
        
        public RepositoryException(ERepositoryCode code, string description) : base(new ExceptionInfo
                                                                                   {
                                                                                       Code        = (int)code,
                                                                                       Description = description,
                                                                                       Domain      = DomainName
                                                                                   })
        {
        }
    }
}