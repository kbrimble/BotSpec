namespace BotSpec.Exceptions
{
    public class CardActionAssertionFailedException : BotAssertionFailedException
    {
        public CardActionAssertionFailedException(string message) : base(message) { }
    }
}