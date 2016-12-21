using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IReceiptCardAssertions : ICanAssertButtons, ICanAssertTapActions, ICanAssertReceiptItems, ICanAssertFacts, IThrow<ReceiptCardAssertionFailedException>
    {
        IReceiptCardAssertions HasTitleMatching(string regex);
        IReceiptCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptCardAssertions HasTotalMatching(string regex);
        IReceiptCardAssertions HasTotalMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptCardAssertions HasTaxMatching(string regex);
        IReceiptCardAssertions HasTaxMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptCardAssertions HasVatMatching(string regex);
        IReceiptCardAssertions HasVatMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}