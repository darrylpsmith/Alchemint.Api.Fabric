using System;

namespace Alchemint.Core.Exceptions
{

    public class RecordUpdateException : Exception
    {
        public RecordUpdateException()
        {
        }

        public RecordUpdateException(string message)
            : base(message)
        {
        }

        public RecordUpdateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
