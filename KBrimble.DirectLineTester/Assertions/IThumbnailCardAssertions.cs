using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KBrimble.DirectLineTester.Assertions
{
    public interface IThumbnailCardAssertions
    {
        IThumbnailCardAssertions HasTitleMatching(string regex);
        IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups);
        IThumbnailCardAssertions HasSubtitleMatching(string regex);
        IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups);
        IThumbnailCardAssertions HasTextMatching(string regex);
        IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups);
    }
}