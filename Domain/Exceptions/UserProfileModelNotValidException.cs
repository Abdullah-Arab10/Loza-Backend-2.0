namespace Loza.Domain.Exceptions
{
    public class UserProfileModelNotValidException : DomainModelInvalidException
    {

        public UserProfileModelNotValidException() { }
        public UserProfileModelNotValidException(string message) : base(message) { }
    }
}
