using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface ICardActionAssertions
    {
        ICardActionAssertions HasTitleMatching(string regex);
        ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasValueMatching(string regex);
        ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasTypeMatching(string regex);
        ICardActionAssertions HasTypeMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions HasImageMatching(string regex);
        ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
    }
}