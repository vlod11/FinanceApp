using System;

namespace Common
{
    public class ContentException : Exception
    {
        public ContentException(ExceptionInfo exceptionInfo)
        {
            ExceptionInfo = exceptionInfo;
        }

        public ExceptionInfo ExceptionInfo { get; protected set; }
    }
}