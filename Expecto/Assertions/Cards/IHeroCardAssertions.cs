using System.Collections.Generic;

namespace Expecto.Assertions.Cards
{
    public interface IHeroCardAssertions : ICanAssertCardImages, ICanAssertButtons, ICanAssertTapActions
    {
        IHeroCardAssertions TitleMatching(string regex);
        IHeroCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IHeroCardAssertions SubtitleMatching(string regex);
        IHeroCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IHeroCardAssertions TextMatching(string regex);
        IHeroCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}