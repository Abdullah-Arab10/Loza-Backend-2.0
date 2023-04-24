
namespace Loza.Domain.Exceptions
{
    public class UserNotFoundException : DomainModelInvalidException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
