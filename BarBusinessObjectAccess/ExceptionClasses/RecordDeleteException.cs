using System;

namespace Alchemint.Core.Exceptions
{

    public class RecordDeleteException : Exception
    {
        public RecordDeleteException()
        {
        }

        public RecordDeleteException(string message)
            : base(message)
        {
        }

        public RecordDeleteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
