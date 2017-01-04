using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface IFactAssertions
    {
        IFactAssertions KeyMatching(string regex);
        IFactAssertions KeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IFactAssertions ValueMatching(string regex);
        IFactAssertions ValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}