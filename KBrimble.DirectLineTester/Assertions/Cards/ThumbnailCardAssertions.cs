using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class ThumbnailCardAssertions : IThumbnailCardAssertions, IThrow<ThumbnailCardAssertionFailedException>
    {
        private readonly ThumbnailCard _thumbnailCard;
        private readonly StringHelpers<ThumbnailCardAssertionFailedException> _stringHelpers;

        public ThumbnailCardAssertions(ThumbnailCard thumbnailCard)
        {
            _thumbnailCard = thumbnailCard;
            _stringHelpers = new StringHelpers<ThumbnailCardAssertionFailedException>();
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_thumbnailCard.Title, regex, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_thumbnailCard.Title, regex, groupMatchRegex, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_thumbnailCard.Subtitle, regex, CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_thumbnailCard.Subtitle, regex, groupMatchRegex, CreateEx(nameof(ThumbnailCard.Subtitle), regex));
            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_thumbnailCard.Text, regex, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_thumbnailCard.Text, regex, groupMatchRegex, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public Func<ThumbnailCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected thumbnail card to have property {testedProperty} to match {regex} but regex test failed.";
            return () => new ThumbnailCardAssertionFailedException(message);
        }

        public ICardImageAssertions WithCardImageThat()
        {
            return new CardImageSetAssertions(_thumbnailCard.Images);
        }

        public ICardActionAssertions WithButtonsThat()
        {
            return new CardActionSetAssertions(_thumbnailCard.Buttons);
        }

        public ICardActionAssertions WithTapActionThat()
        {
            return new CardActionAssertions(_thumbnailCard.Tap);
        }
    }
}