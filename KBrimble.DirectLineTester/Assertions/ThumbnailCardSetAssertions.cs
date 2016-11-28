using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{
    public class ThumbnailCardSetAssertions : IThumbnailCardAssertions
    {
        readonly IEnumerable<ThumbnailCard> thumbnailCards;

        public ThumbnailCardSetAssertions(IEnumerable<ThumbnailCard> thumbnailCards)
        {
            this.thumbnailCards = thumbnailCards;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            var passedAssertion = false;
            foreach (var thumbnailCard in thumbnailCards)
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

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var thumbnailCard in thumbnailCards)
            {
                try
                {
                    IEnumerable<string> matches;
                    thumbnailCard.Should().HasSubtitleMatching(regex, groupMatchRegex, out matches);
                    if (matches.Any())
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
            var passedAssertion = false;
            foreach (var thumbnailCard in thumbnailCards)
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

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var thumbnailCard in thumbnailCards)
            {
                try
                {
                    IEnumerable<string> matches;
                    thumbnailCard.Should().HasTextMatching(regex, groupMatchRegex, out matches);
                    if (matches.Any())
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

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            var passedAssertion = false;
            foreach (var thumbnailCard in thumbnailCards)
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

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var thumbnailCard in thumbnailCards)
            {
                try
                {
                    IEnumerable<string> matches;
                    thumbnailCard.Should().HasTitleMatching(regex, groupMatchRegex, out matches);
                    if (matches.Any())
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
    }
}
