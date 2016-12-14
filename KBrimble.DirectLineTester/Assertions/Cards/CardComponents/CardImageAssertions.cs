using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public class CardImageAssertions : ICardImageAssertions, IThrow<CardImageAssertionFailedException>
    {
        private readonly CardImage _cardImage;
        private readonly StringHelpers<CardImageAssertionFailedException> _stringHelpers;

        public CardImageAssertions(CardImage cardImage) : this()
        {
            if (cardImage == null)
                throw new ArgumentNullException(nameof(cardImage));

            _cardImage = cardImage;
        }

        public CardImageAssertions(IHaveAnImage iHaveAnImage) : this()
        {
            if (iHaveAnImage == null)
                throw new ArgumentNullException(nameof(iHaveAnImage));
            _cardImage = iHaveAnImage.Image;
        }

        public CardImageAssertions()
        {
            _stringHelpers = new StringHelpers<CardImageAssertionFailedException>();
        }

        public ICardImageAssertions HasUrlMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_cardImage.Url, regex, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions HasUrlMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            groupMatches = _stringHelpers.TestForMatchAndReturnGroups(_cardImage.Url, regex, groupMatchRegex, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions HasAltMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_cardImage.Alt, regex, CreateEx(nameof(CardImage.Alt), regex));

            return this;
        }

        public ICardImageAssertions HasAltMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            groupMatches = _stringHelpers.TestForMatchAndReturnGroups(_cardImage.Alt, regex, groupMatchRegex, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public Func<CardImageAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected card image to have property {testedProperty} to match {regex} but regex test failed.";
            return () => new CardImageAssertionFailedException(message);
        }
    }
}
