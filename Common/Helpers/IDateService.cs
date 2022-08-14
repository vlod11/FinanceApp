using System;

namespace Common.Helpers
{
    public interface IDateService
    {
        DateTime GetDateTimeNow();
        DateTime GetDateTimeUtcNow();
    }
}