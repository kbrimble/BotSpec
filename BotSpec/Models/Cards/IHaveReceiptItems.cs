using System.Collections.Generic;

namespace BotSpec.Models.Cards
{
    internal interface IHaveReceiptItems
    {
        IList<ReceiptItem> Items { get; set; }
    }
}