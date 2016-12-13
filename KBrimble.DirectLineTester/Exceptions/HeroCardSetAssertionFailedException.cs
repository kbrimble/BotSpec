namespace KBrimble.DirectLineTester.Exceptions
{
    public class HeroCardSetAssertionFailedException : BotAssertionFailedException
    {
        public HeroCardSetAssertionFailedException(string message) : base(message)
        {
        }
    }
}