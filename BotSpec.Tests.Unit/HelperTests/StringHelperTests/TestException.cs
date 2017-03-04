using BotSpec.Exceptions;

namespace BotSpec.Tests.Unit.HelperTests.StringHelperTests
{
    public class TestException : BotAssertionFailedException
    {
        public TestException(string message) : base(message) {}
    }
}