using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using static KBrimble.DirectLineTester.Assertions.StringHelpers;

namespace KBrimble.DirectLineTester.Assertions.Cards
{

    public class ThumbnailCardAssertions : IThumbnailCardAssertions
    {
        private readonly ThumbnailCard _thumbnailCard;

        public ThumbnailCardAssertions(ThumbnailCard thumbnailCard)
        {
            _thumbnailCard = thumbnailCard;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestForMatch(_thumbnailCard.Title, regex, new ThumbnailCardAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = TestForMatchAndReturnGroups(_thumbnailCard.Title, regex, groupMatchRegex, new ThumbnailCardAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestForMatch(_thumbnailCard.Subtitle, regex, new ThumbnailCardAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = TestForMatchAndReturnGroups(_thumbnailCard.Subtitle, regex, groupMatchRegex, new ThumbnailCardAssertionFailedException());
            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestForMatch(_thumbnailCard.Text, regex, new ThumbnailCardAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = TestForMatchAndReturnGroups(_thumbnailCard.Text, regex, groupMatchRegex, new ThumbnailCardAssertionFailedException());

            return this;
        }
    }
}