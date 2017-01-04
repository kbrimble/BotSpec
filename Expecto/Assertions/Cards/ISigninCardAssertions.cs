using System.Collections.Generic;

namespace Expecto.Assertions.Cards
{
    public interface ISigninCardAssertions : ICanAssertButtons
    {
        ISigninCardAssertions TextMatching(string regex);
        ISigninCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}