using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{

    public class ThumbnailCardAssertions : IThumbnailCardAssertions
    {
        readonly ThumbnailCard thumbnailCard;

        public ThumbnailCardAssertions(ThumbnailCard thumbnailCard)
        {
            this.thumbnailCard = thumbnailCard;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (!Regex.IsMatch(thumbnailCard.Title, regex))
                throw new ThumbnailCardAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;

            if (!Regex.IsMatch(thumbnailCard.Title, regex, RegexOptions.IgnoreCase))
                throw new MessageAssertionFailedException();

            var matches = Regex.Matches(thumbnailCard.Title, groupMatchRegex).Cast<Match>().Select(match => match.Value);
            if (matches.Any())
                matchedGroups = matches;

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (!Regex.IsMatch(thumbnailCard.Subtitle, regex))
                throw new ThumbnailCardAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;

            if (!Regex.IsMatch(thumbnailCard.Subtitle, regex, RegexOptions.IgnoreCase))
                throw new MessageAssertionFailedException();

            var matches = Regex.Matches(thumbnailCard.Subtitle, groupMatchRegex).Cast<Match>().Select(match => match.Value);
            if (matches.Any())
                matchedGroups = matches;

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (!Regex.IsMatch(thumbnailCard.Text, regex))
                throw new ThumbnailCardAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;

            if (!Regex.IsMatch(thumbnailCard.Text, regex, RegexOptions.IgnoreCase))
                throw new MessageAssertionFailedException();

            var matches = Regex.Matches(thumbnailCard.Text, groupMatchRegex).Cast<Match>().Select(match => match.Value);
            if (matches.Any())
                matchedGroups = matches;

            return this;
        }
    }
}