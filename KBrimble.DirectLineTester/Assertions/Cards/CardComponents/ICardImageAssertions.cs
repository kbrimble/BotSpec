using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface ICardImageAssertions
    {
        ICardImageAssertions UrlMatching(string regex);
        ICardImageAssertions UrlMatching(string regex, string groupMatchRegex, out IList<string> groupMatches);
        ICardImageAssertions AltMatching(string regex);
        ICardImageAssertions AltMatching(string regex, string groupMatchRegex, out IList<string> groupMatches);
    }
}