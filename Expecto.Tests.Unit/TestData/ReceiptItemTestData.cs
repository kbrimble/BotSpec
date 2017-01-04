using System.Collections.Generic;
using Expecto.Models.Cards;

namespace Expecto.Tests.Unit.TestData
{
    public class ReceiptItemTestData
    {
        internal static IEnumerable<ReceiptItem> CreateReceiptItemSetWithOneItemThatHasSetProperties(string title = default(string), string subtitle = default(string), string text = default(string), CardImage image = default(CardImage), string price = default(string), string quantity = default(string), CardAction tap = default(CardAction))
        {
            var matchingItem = new ReceiptItem(title, subtitle, text, image, price, quantity, tap);
            var items = CreateRandomReceiptItems();
            items.Add(matchingItem);
            return items;
        }
        internal static IEnumerable<ReceiptItem> CreateReceiptItemSetWithAllItemsWithSetProperties(string title = default(string), string subtitle = default(string), string text = default(string), CardImage image = default(CardImage), string price = default(string), string quantity = default(string), CardAction tap = default(CardAction))
        {
            var items = new List<ReceiptItem>();
            for (var i = 0; i < 5; i++)
            {
                var matchingItem = new ReceiptItem(title, subtitle, text, image, price, quantity, tap);
                items.Add(matchingItem);
            }
            return items;
        }

        internal static List<ReceiptItem> CreateRandomReceiptItems()
        {
            var items = new List<ReceiptItem>();
            for (var i = 0; i < 5; i++)
            {
                items.Add(new ReceiptItem($"title{i}", $"subtitle{i}", $"text{i}", null, $"{i:c}", $"{i}"));
            }
            return items;
        }
    }
}