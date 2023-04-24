namespace Loza.Domain.Exceptions
{
    public class UserProfileModelNotValidException : DomainModelInvalidException
    {

        public UserProfileModelNotValidException() { }
        public UserProfileModelNotValidException(string message) : base(message) { }
        public UserProfileModelNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
