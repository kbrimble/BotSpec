using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IThumbnailCardAssertions : ICanAssertCardImages, ICanAssertButtons, ICanAssertTapActions, IThrow<ThumbnailCardAssertionFailedException>
    {
        IThumbnailCardAssertions TitleMatching(string regex);
        IThumbnailCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IThumbnailCardAssertions SubtitleMatching(string regex);
        IThumbnailCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IThumbnailCardAssertions TextMatching(string regex);
        IThumbnailCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}