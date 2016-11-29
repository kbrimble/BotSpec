using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class CardImageSetAssertions : ICardImageAssertions, IThrow<CardImageSetAssertionFailedException>
    {
        private readonly IList<CardImage> _cardImages;
        private readonly SetHelpers<CardImage, CardImageAssertionFailedException, CardImageSetAssertionFailedException> _setHelpers;

        public CardImageSetAssertions(IList<CardImage> cardImages)
        {
            _cardImages = cardImages;
            _setHelpers = new SetHelpers<CardImage, CardImageAssertionFailedException, CardImageSetAssertionFailedException>();
        }

        public ICardImageAssertions HasUrlMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_cardImages, cardImage => cardImage.That().HasUrlMatching(regex), CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions HasUrlMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            throw new NotImplementedException();
        }

        public ICardImageAssertions HasAltMatching(string regex)
        {
            throw new NotImplementedException();
        }

        public ICardImageAssertions HasAltMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            throw new NotImplementedException();
        }

        public CardImageSetAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            throw new NotImplementedException();
        }
    }
}
