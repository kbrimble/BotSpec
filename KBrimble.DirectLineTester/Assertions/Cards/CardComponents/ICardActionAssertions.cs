using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface ICardActionAssertions : IThrow<CardActionAssertionFailedException>
    {
        ICardActionAssertions HasTitleMatching(string regex);
        ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasValueMatching(string regex);
        ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasImageMatching(string regex);
        ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasType(CardActionType type);
    }
}