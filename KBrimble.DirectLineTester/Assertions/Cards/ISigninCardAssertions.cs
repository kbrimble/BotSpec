using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface ISigninCardAssertions : ICanAssertButtons, IThrow<SigninCardAssertionFailedException>
    {
        ISigninCardAssertions TextMatching(string regex);
        ISigninCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}