using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface IReceiptItemAssertions : ICanAssertTapActions, ICanAssertCardImages, IThrow<ReceiptItemAssertionFailedException>
    {
        IReceiptItemAssertions HasTitleMatching(string regex);
        IReceiptItemAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions HasSubtitleMatching(string regex);
        IReceiptItemAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions HasTextMatching(string regex);
        IReceiptItemAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions HasPriceMatching(string regex);
        IReceiptItemAssertions HasPriceMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions HasQuantityMatching(string regex);
        IReceiptItemAssertions HasQuantityMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}