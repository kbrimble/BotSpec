using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    internal interface IHaveReceiptItems
    {
        IList<ReceiptItem> Items { get; set; }
    }
}