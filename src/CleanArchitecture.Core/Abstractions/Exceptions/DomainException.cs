
namespace CleanArchitecture.Core.Abstractions.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
