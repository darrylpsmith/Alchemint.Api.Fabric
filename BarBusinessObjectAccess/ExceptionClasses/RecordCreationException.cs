using System;

namespace Alchemint.Core.Exceptions
{

    public class RecordCreationException : Exception
    {
        public RecordCreationException()
        {
        }

        public RecordCreationException(string message)
            : base(message)
        {
        }

        public RecordCreationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
