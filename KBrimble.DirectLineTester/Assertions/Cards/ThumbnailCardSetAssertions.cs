using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

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

            var passedAssertion = false;
            foreach (var thumbnailCard in _thumbnailCards)
            {
                try
                {
                    thumbnailCard.Should().HasSubtitleMatching(regex);
                }
                catch (ThumbnailCardAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var thumbnailCard in _thumbnailCards)
            {
                try
                {
                    IList<string> matches;
                    thumbnailCard.Should().HasSubtitleMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (ThumbnailCardAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            var passedAssertion = false;
            foreach (var thumbnailCard in _thumbnailCards)
            {
                try
                {
                    thumbnailCard.Should().HasTextMatching(regex);
                }
                catch (ThumbnailCardAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var thumbnailCard in _thumbnailCards)
            {
                try
                {
                    IList<string> matches;
                    thumbnailCard.Should().HasTextMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (ThumbnailCardAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            var passedAssertion = false;
            foreach (var thumbnailCard in _thumbnailCards)
            {
                try
                {
                    thumbnailCard.Should().HasTitleMatching(regex);
                }
                catch (ThumbnailCardAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var thumbnailCard in _thumbnailCards)
            {
                try
                {
                    IList<string> matches;
                    thumbnailCard.Should().HasTitleMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (ThumbnailCardAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;

            return this;
        }
    }
}
