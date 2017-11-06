using BotSpec.Assertions.Cards.CardComponents;
using System.Collections.Generic;

namespace BotSpec.Assertions.Activities
{
    public interface IActivityAssertions : ICanAssertActivityAttachments
    {
        IActivityAssertions TextMatching(string regex);
        IActivityAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IActivityAssertions IdMatching(string regex);
        IActivityAssertions IdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IActivityAssertions FromMatching(string regex);
        IActivityAssertions FromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        ICardActionAssertions WithSuggestedActions();
    }
}
