
namespace Application.Behaviours.Exceptions
{
    public class AccountValidationFailureException : Exception
    {
        public AccountValidationFailureException(string message) : base(message) { }
    }
}
