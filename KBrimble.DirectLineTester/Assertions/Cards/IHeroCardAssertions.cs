using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IHeroCardAssertions : ICanAssertCardImages, ICanAssertButtons, ICanAssertTapActions
    {
        IHeroCardAssertions HasTitleMatching(string regex);
        IHeroCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IHeroCardAssertions HasSubtitleMatching(string regex);
        IHeroCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IHeroCardAssertions HasTextMatching(string regex);
        IHeroCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}