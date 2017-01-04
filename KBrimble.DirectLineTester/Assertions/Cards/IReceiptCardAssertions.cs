using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IReceiptCardAssertions : ICanAssertButtons, ICanAssertTapActions, ICanAssertReceiptItems, ICanAssertFacts, IThrow<ReceiptCardAssertionFailedException>
    {
        IReceiptCardAssertions TitleMatching(string regex);
        IReceiptCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptCardAssertions TotalMatching(string regex);
        IReceiptCardAssertions TotalMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptCardAssertions TaxMatching(string regex);
        IReceiptCardAssertions TaxMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptCardAssertions VatMatching(string regex);
        IReceiptCardAssertions VatMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}