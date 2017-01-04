using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public interface IReceiptItemAssertions : ICanAssertTapActions, ICanAssertCardImages, IThrow<ReceiptItemAssertionFailedException>
    {
        IReceiptItemAssertions TitleMatching(string regex);
        IReceiptItemAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions SubtitleMatching(string regex);
        IReceiptItemAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions TextMatching(string regex);
        IReceiptItemAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions PriceMatching(string regex);
        IReceiptItemAssertions PriceMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IReceiptItemAssertions QuantityMatching(string regex);
        IReceiptItemAssertions QuantityMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}