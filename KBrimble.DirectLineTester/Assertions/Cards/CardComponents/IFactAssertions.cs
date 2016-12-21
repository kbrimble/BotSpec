using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface IFactAssertions : IThrow<FactAssertionFailedException>
    {
        IFactAssertions HasKeyMatching(string regex);
        IFactAssertions HasKeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IFactAssertions HasValueMatching(string regex);
        IFactAssertions HasValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}