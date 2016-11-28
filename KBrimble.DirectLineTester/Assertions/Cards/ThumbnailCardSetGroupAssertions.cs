using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class ThumbnailCardSetGroupAssertions : IThumbnailCardAssertions
    {
        private readonly IList<ThumbnailCardSetAssertions> _thumbnailCardSets;

        public ThumbnailCardSetGroupAssertions(MessageSet messageSet) : this(messageSet.Messages) {}

        public ThumbnailCardSetGroupAssertions(IEnumerable<Message> messageSet)
        {
            _thumbnailCardSets = new List<ThumbnailCardSetAssertions>();
            foreach (var message in messageSet)
            {
                _thumbnailCardSets.Add(new ThumbnailCardSetAssertions(message));
            }
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            var assertionPassed = false;
            foreach (var carSet in _thumbnailCardSets)
            {
                try
                {
                    carSet.HasTitleMatching(regex);
                }
                catch (ThumbnailCardSetAssertionFailedException)
                {
                    continue;
                }
                assertionPassed = true;
                break;
            }

            if (!assertionPassed)
                throw new ThumbnailCardSetGroupAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var cardSet in _thumbnailCardSets)
            {
                try
                {
                    IList<string> matches;
                    cardSet.HasSubtitleMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (ThumbnailCardSetAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetGroupAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            var passedAssertion = false;
            foreach (var cardSet in _thumbnailCardSets)
            {
                try
                {
                    cardSet.HasSubtitleMatching(regex);
                }
                catch (ThumbnailCardSetAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetGroupAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var cardSet in _thumbnailCardSets)
            {
                try
                {
                    IList<string> matches;
                    cardSet.HasSubtitleMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (ThumbnailCardSetAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetGroupAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            var passedAssertion = false;
            foreach (var cardSet in _thumbnailCardSets)
            {
                try
                {
                    cardSet.HasTextMatching(regex);
                }
                catch (ThumbnailCardSetAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetGroupAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var cardSet in _thumbnailCardSets)
            {
                try
                {
                    IList<string> matches;
                    cardSet.HasTextMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (ThumbnailCardSetAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new ThumbnailCardSetGroupAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;

            return this;
        }
    }
}