using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IThumbnailCardAssertions : ICanAssertCardImages
    {
        IThumbnailCardAssertions HasTitleMatching(string regex);
        IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IThumbnailCardAssertions HasSubtitleMatching(string regex);
        IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IThumbnailCardAssertions HasTextMatching(string regex);
        IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}