namespace BBIS.Common.Exceptions
{
    public class RecordExistException : Exception
    {
        public RecordExistException() { }
        public RecordExistException(string message) : base(message) { }
        public RecordExistException(string message, Exception inner) : base(message, inner) { }
    }

    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() { }
        public RecordNotFoundException(string message) : base(message) { }
        public RecordNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

    public class RefreshTokenException : Exception
    {
        public RefreshTokenException() { }
        public RefreshTokenException(string message) : base(message) { }
        public RefreshTokenException(string message, Exception inner) : base(message, inner) { }
    }
}
