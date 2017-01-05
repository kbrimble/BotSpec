namespace BotSpec.Exceptions
{
    public class HeroCardAssertionFailedException : BotAssertionFailedException
    {
        public HeroCardAssertionFailedException(string message) : base(message) { }
    }
}