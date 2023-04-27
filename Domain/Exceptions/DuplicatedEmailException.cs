
namespace Loza.Domain.Exceptions
{
    public class DuplicatedEmailException : DomainModelInvalidException
    {
        public DuplicatedEmailException() { }
        public DuplicatedEmailException(string message) : base(message) { }
    }
}
