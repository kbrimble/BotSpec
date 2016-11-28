using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

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

            if (_thumbnailCard.Title == null || !Regex.IsMatch(_thumbnailCard.Title.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw new ThumbnailCardAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;

            HasTitleMatching(regex);

            var matches = Regex.Matches(_thumbnailCard.Title, groupMatchRegex).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            if (_thumbnailCard.Subtitle == null || !Regex.IsMatch(_thumbnailCard.Subtitle.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw new ThumbnailCardAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;

            HasSubtitleMatching(regex);

            var matches = Regex.Matches(_thumbnailCard.Subtitle, groupMatchRegex).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            if (_thumbnailCard.Text == null || !Regex.IsMatch(_thumbnailCard.Text.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw new ThumbnailCardAssertionFailedException();

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;

            HasTextMatching(regex);

            var matches = Regex.Matches(_thumbnailCard.Text, groupMatchRegex).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

            return this;
        }
    }
}