
namespace Loza.Domain.Exceptions
{
    public class IncorrectPasswordException : DomainModelInvalidException
    {
        public IncorrectPasswordException() { }
        public IncorrectPasswordException(string message) : base(message) { }
   
    }
}
