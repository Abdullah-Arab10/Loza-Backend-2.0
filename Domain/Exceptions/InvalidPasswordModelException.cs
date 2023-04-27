
namespace Loza.Domain.Exceptions
{
    public class InvalidPasswordModelException : DomainModelInvalidException
    {
        public InvalidPasswordModelException() { }
        public InvalidPasswordModelException(string message) : base(message) { }
    }
}
