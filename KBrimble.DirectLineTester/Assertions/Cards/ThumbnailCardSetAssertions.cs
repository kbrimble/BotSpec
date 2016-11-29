using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;
using static KBrimble.DirectLineTester.Assertions.SetHelpers;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class ThumbnailCardSetAssertions : IThumbnailCardAssertions
    {
        private readonly IEnumerable<ThumbnailCard> _thumbnailCards;

        public ThumbnailCardSetAssertions(Message message)
        {
            _thumbnailCards = AttachmentExtractor.ExtractThumbnailCardsFromMessage(message);
        }

        public ThumbnailCardSetAssertions(IEnumerable<ThumbnailCard> thumbnailCards)
        {
            _thumbnailCards = thumbnailCards;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            
            TestSetForMatch(_thumbnailCards, card => card.Should().HasSubtitleMatching(regex), typeof(ThumbnailCardAssertionFailedException), new ThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<ThumbnailCard> act = (ThumbnailCard card, out IList<string> matches) => card.Should().HasSubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_thumbnailCards, act, typeof(ThumbnailCardAssertionFailedException), new ThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestSetForMatch(_thumbnailCards, card => card.Should().HasTextMatching(regex), typeof(ThumbnailCardAssertionFailedException), new ThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<ThumbnailCard> act = (ThumbnailCard card, out IList<string> matches) => card.Should().HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_thumbnailCards, act, typeof(ThumbnailCardAssertionFailedException), new ThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestSetForMatch(_thumbnailCards, card => card.Should().HasTitleMatching(regex), typeof(ThumbnailCardAssertionFailedException), new ThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<ThumbnailCard> act = (ThumbnailCard card, out IList<string> matches) => card.Should().HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_thumbnailCards, act, typeof(ThumbnailCardAssertionFailedException), new ThumbnailCardSetAssertionFailedException());

            return this;
        }
    }
}
