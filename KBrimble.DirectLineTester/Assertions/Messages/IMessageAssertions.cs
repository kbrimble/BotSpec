using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public interface IMessageAssertions : ICanAssertMessageAttachments
    {
        IMessageAssertions HaveTextMatching(string regex);
        IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions HaveIdMatching(string regex);
        IMessageAssertions HaveIdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions HaveFromMatching(string regex);
        IMessageAssertions HaveFromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}
