using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class GroupOfThumbnailCardSetAssertions : IThumbnailCardAssertions, IThrow<GroupOfThumbnailCardSetAssertionFailedException>
    {
        private readonly IList<ThumbnailCardSetAssertions> _thumbnailCardSets;
        private readonly SetHelpers<ThumbnailCardSetAssertions, ThumbnailCardSetAssertionFailedException, GroupOfThumbnailCardSetAssertionFailedException> _setHelpers;

        public GroupOfThumbnailCardSetAssertions(MessageSet messageSet) : this(messageSet.Messages) {}

        public GroupOfThumbnailCardSetAssertions(IEnumerable<Message> messageSet) : this()
        {
            _thumbnailCardSets = new List<ThumbnailCardSetAssertions>();
            foreach (var message in messageSet)
            {
                _thumbnailCardSets.Add(new ThumbnailCardSetAssertions(message));
            }
        }

        public GroupOfThumbnailCardSetAssertions(IList<ThumbnailCardSetAssertions> cardSets) : this()
        {
            _thumbnailCardSets = cardSets;
        }

        private GroupOfThumbnailCardSetAssertions()
        {
            _setHelpers = new SetHelpers<ThumbnailCardSetAssertions, ThumbnailCardSetAssertionFailedException, GroupOfThumbnailCardSetAssertionFailedException>();
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_thumbnailCardSets, cardSet => cardSet.HasTitleMatching(regex), CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCardSetAssertions, ThumbnailCardSetAssertionFailedException, GroupOfThumbnailCardSetAssertionFailedException>.TestWithGroups act
                = (ThumbnailCardSetAssertions cardSet, out IList<string> matches) => cardSet.HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_thumbnailCardSets, act, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_thumbnailCardSets, cardSet => cardSet.HasSubtitleMatching(regex), CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCardSetAssertions, ThumbnailCardSetAssertionFailedException, GroupOfThumbnailCardSetAssertionFailedException>.TestWithGroups act
                = (ThumbnailCardSetAssertions cardSet, out IList<string> matches) => cardSet.HasSubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_thumbnailCardSets, act, CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_thumbnailCardSets, cardSet => cardSet.HasTextMatching(regex), CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCardSetAssertions, ThumbnailCardSetAssertionFailedException, GroupOfThumbnailCardSetAssertionFailedException>.TestWithGroups act
                = (ThumbnailCardSetAssertions cardSet, out IList<string> matches) => cardSet.HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_thumbnailCardSets, act, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public GroupOfThumbnailCardSetAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one thumbnail card in any set to have property {testedProperty} to match {regex} but none did.";
            return new GroupOfThumbnailCardSetAssertionFailedException(message);
        }
    }
}