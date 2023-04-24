
namespace Loza.Domain.Exceptions
{
    public class IncorrectPasswordException : DomainModelInvalidException
    {
        public IncorrectPasswordException() { }
        public IncorrectPasswordException(string message) : base(message) { }
        public IncorrectPasswordException(string message, Exception inner) : base(message, inner) { }
    }
}
