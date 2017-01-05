namespace BotSpec.Exceptions
{
    public class CardImageAssertionFailedException : BotAssertionFailedException
    {
        public CardImageAssertionFailedException(string message) : base(message) {}
    }
}