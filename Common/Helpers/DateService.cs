using System;

namespace Common.Helpers
{
    public class DateService : IDateService
    {
        public DateTime GetDateTimeNow() => DateTime.Now;
        public DateTime GetDateTimeUtcNow() => DateTime.UtcNow;
    }
}