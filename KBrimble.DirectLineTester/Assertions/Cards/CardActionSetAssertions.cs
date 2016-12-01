using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class CardActionSetAssertions : ICardActionAssertions, IThrow<CardActionSetAssertionFailedException>
    {
        private readonly IList<CardAction> _cardActions;

        public CardActionSetAssertions(IList<CardAction> cardActions)
        {
            _cardActions = cardActions;
        }

        public ICardActionAssertions HasTitleMatching(string regex)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasValueMatching(string regex)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasTypeMatching(string regex)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasTypeMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasImageMatching(string regex)
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new System.NotImplementedException();
        }

        public CardActionSetAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            throw new System.NotImplementedException();
        }
    }
}