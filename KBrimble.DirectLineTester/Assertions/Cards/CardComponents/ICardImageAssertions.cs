using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface ICardImageAssertions
    {
        ICardImageAssertions HasUrlMatching(string regex);
        ICardImageAssertions HasUrlMatching(string regex, string groupMatchRegex, out IList<string> groupMatches);
        ICardImageAssertions HasAltMatching(string regex);
        ICardImageAssertions HasAltMatching(string regex, string groupMatchRegex, out IList<string> groupMatches);
    }
}