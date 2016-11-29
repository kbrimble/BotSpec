using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using static KBrimble.DirectLineTester.Assertions.SetHelpers;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class GroupOfThumbnailCardSetAssertions : IThumbnailCardAssertions
    {
        private readonly IList<ThumbnailCardSetAssertions> _thumbnailCardSets;

        public GroupOfThumbnailCardSetAssertions(MessageSet messageSet) : this(messageSet.Messages) {}

        public GroupOfThumbnailCardSetAssertions(IEnumerable<Message> messageSet)
        {
            _thumbnailCardSets = new List<ThumbnailCardSetAssertions>();
            foreach (var message in messageSet)
            {
                _thumbnailCardSets.Add(new ThumbnailCardSetAssertions(message));
            }
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestSetForMatch(_thumbnailCardSets, cardSet => cardSet.HasTitleMatching(regex), typeof(ThumbnailCardSetAssertionFailedException), new GroupOfThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<ThumbnailCardSetAssertions> act = (ThumbnailCardSetAssertions cardSet, out IList<string> matches) => cardSet.HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_thumbnailCardSets, act, typeof(ThumbnailCardSetAssertionFailedException), new GroupOfThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestSetForMatch(_thumbnailCardSets, cardSet => cardSet.HasSubtitleMatching(regex), typeof(ThumbnailCardSetAssertionFailedException), new GroupOfThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<ThumbnailCardSetAssertions> act = (ThumbnailCardSetAssertions cardSet, out IList<string> matches) => cardSet.HasSubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_thumbnailCardSets, act, typeof(ThumbnailCardSetAssertionFailedException), new GroupOfThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            TestSetForMatch(_thumbnailCardSets, cardSet => cardSet.HasTextMatching(regex), typeof(ThumbnailCardSetAssertionFailedException), new GroupOfThumbnailCardSetAssertionFailedException());

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<ThumbnailCardSetAssertions> act = (ThumbnailCardSetAssertions cardSet, out IList<string> matches) => cardSet.HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_thumbnailCardSets, act, typeof(ThumbnailCardSetAssertionFailedException), new GroupOfThumbnailCardSetAssertionFailedException());

            return this;
        }
    }
}