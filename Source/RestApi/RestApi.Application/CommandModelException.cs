using System;

namespace RestApi.Application
{
    public class CommandModelException : Exception
    {
        public string Property { get; }

        public CommandModelException(string property, string message) : base(message)
        {
            Property = property;
        }
    }
}
