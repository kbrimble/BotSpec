using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface IFactAssertions : IThrow<FactAssertionFailedException>
    {
        IFactAssertions KeyMatching(string regex);
        IFactAssertions KeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IFactAssertions ValueMatching(string regex);
        IFactAssertions ValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}