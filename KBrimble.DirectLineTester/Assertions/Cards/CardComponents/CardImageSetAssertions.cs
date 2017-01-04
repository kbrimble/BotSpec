using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    internal class CardImageSetAssertions : ICardImageAssertions, IThrow<CardImageAssertionFailedException>
    {
        public readonly IList<CardImage> CardImages;
        private readonly SetHelpers<CardImage, CardImageAssertionFailedException> _setHelpers;

        public CardImageSetAssertions(IEnumerable<IHaveAnImage> hasAnImages)
        {
            CardImages = hasAnImages.Select(x => x?.Image).Where(x => x != null).ToList();
        }

        public CardImageSetAssertions(IEnumerable<IHaveImages> hasCardImages) : this()
        {
            CardImages = hasCardImages.Where(x => x?.Images != null).Select(x => x.Images).SelectMany(x => x).ToList();
        }

        public CardImageSetAssertions(IList<CardImage> cardImages) : this()
        {
            CardImages = cardImages.Where(x => x != null).ToList();
        }

        public CardImageSetAssertions()
        {
            _setHelpers = new SetHelpers<CardImage, CardImageAssertionFailedException>();
        }

        public ICardImageAssertions UrlMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(CardImages, cardImage => cardImage.That().UrlMatching(regex), CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions UrlMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<CardImage, CardImageAssertionFailedException>.TestWithGroups act =
                (CardImage item, out IList<string> matches) => item.That().UrlMatching(regex, groupMatchRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(CardImages, act, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions AltMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(CardImages, cardImage => cardImage.That().AltMatching(regex), CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public ICardImageAssertions AltMatching(string regex, string groupMatchRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<CardImage, CardImageAssertionFailedException>.TestWithGroups act =
                (CardImage item, out IList<string> matches) => item.That().AltMatching(regex, groupMatchRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(CardImages, act, CreateEx(nameof(CardImage.Url), regex));

            return this;
        }

        public Func<CardImageAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected a card image to have property {testedProperty} matching {regex} but found none.";
            return () => new CardImageAssertionFailedException(message);
        }
    }
}
