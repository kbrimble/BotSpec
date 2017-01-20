using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Tests.Unit.TestData
{
    public class ReceiptCardTestData
    {
        internal static IEnumerable<ReceiptCard> CreateReceiptCardSetWithOneCardThatHasSetProperties(string title = default(string), IList<ReceiptItem> items = default(IList<ReceiptItem>), IList<Fact> facts = default(IList<Fact>), CardAction tap = default(CardAction), string total = default(string), string tax = default(string), string vat = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            var matchingCard = new ReceiptCard(title, items, facts, tap, total, tax, vat, buttons);
            var cards = CreateRandomReceiptCards();
            cards.Add(matchingCard);
            return cards;
        }
        internal static IEnumerable<ReceiptCard> CreateReceiptCardSetWithAllCardsWithSetProperties(string title = default(string), IList<ReceiptItem> items = default(IList<ReceiptItem>), IList<Fact> facts = default(IList<Fact>), CardAction tap = default(CardAction), string total = default(string), string tax = default(string), string vat = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            var cards = new List<ReceiptCard>();
            for (var i = 0; i < 5; i++)
            {
                var matchingCard = new ReceiptCard(title, items, facts, tap, total, tax, vat, buttons);
                cards.Add(matchingCard);
            }
            return cards;
        }

        internal static List<ReceiptCard> CreateRandomReceiptCards()
        {
            var cards = new List<ReceiptCard>();
            for (var i = 0; i < 5; i++)
            {
                cards.Add(new ReceiptCard($"title{i}", null, null,null, $"{i:c}", $"{i:c}", $"{i:c}"));
            }
            return cards;
        }
    }
}