using System.Collections.Generic;

namespace BotSpec.Assertions.Messages
{
    public interface IMessageAssertions : ICanAssertMessageAttachments
    {
        IMessageAssertions TextMatching(string regex);
        IMessageAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions IdMatching(string regex);
        IMessageAssertions IdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions FromMatching(string regex);
        IMessageAssertions FromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}
