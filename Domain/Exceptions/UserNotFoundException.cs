
namespace Loza.Domain.Exceptions
{
    public class UserNotFoundException : DomainModelInvalidException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
    }
}
