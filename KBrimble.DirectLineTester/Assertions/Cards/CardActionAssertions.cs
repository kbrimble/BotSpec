using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class CardActionAssertions : ICardActionAssertions, IThrow<CardActionAssertionFailedException>
    {
        private readonly CardAction _cardAction;

        public CardActionAssertions(CardAction cardAction)
        {
            _cardAction = cardAction;
        }

        public ICardActionAssertions HasTitleMatching(string regex)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasValueMatching(string regex)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasTypeMatching(string regex)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasTypeMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasImageMatching(string regex)
        {
            throw new NotImplementedException();
        }

        public ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            throw new NotImplementedException();
        }

        public CardActionAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICardActionAssertions
    {
        ICardActionAssertions HasTitleMatching(string regex);
        ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasValueMatching(string regex);
        ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasTypeMatching(string regex);
        ICardActionAssertions HasTypeMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasImageMatching(string regex);
        ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);

    }
}
