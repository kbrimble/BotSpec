namespace BotSpec.Exceptions
{
    public class SigninCardAssertionFailedException : BotAssertionFailedException
    {
        public SigninCardAssertionFailedException(string message) : base(message) {}
    }
}