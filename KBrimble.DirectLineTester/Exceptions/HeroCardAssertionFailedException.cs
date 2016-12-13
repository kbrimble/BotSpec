namespace KBrimble.DirectLineTester.Exceptions
{
    public class HeroCardAssertionFailedException : BotAssertionFailedException
    {
        public HeroCardAssertionFailedException(string message) : base(message) { }
    }
}