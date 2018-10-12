using System;

namespace Alchemint.Core.Exceptions
{

    public class RecordRetrieveException : Exception
    {
        public RecordRetrieveException()
        {
        }

        public RecordRetrieveException(string message)
            : base(message)
        {
        }

        public RecordRetrieveException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
