using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class CardImageAssertions : ICardImageAssertions, IThrow<CardImageAssertionFailedException>
    {
        private readonly CardImage _cardImage;

        public CardImageAssertions(CardImage cardImage)
        {
            _cardImage = cardImage;
        }

        public ICardImageAssertions HasUrlMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_cardImage.Url, regex, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions HasUrlMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            groupMatches = StringHelpers.TestForMatchAndReturnGroups(_cardImage.Url, regex, groupMatchRegex, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions HasAltMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_cardImage.Alt, regex, CreateEx(nameof(CardImage.Alt), regex));

            return this;
        }

        public ICardImageAssertions HasAltMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            groupMatches = StringHelpers.TestForMatchAndReturnGroups(_cardImage.Alt, regex, groupMatchRegex, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public CardImageAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected card image to have property {testedProperty} to match {regex} but regex test failed.";
            return new CardImageAssertionFailedException(message);
        }
    }
}
