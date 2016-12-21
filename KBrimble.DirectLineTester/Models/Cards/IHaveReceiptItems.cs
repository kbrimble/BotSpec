using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    public interface IHaveReceiptItems
    {
        IList<ReceiptItem> Items { get; set; }
    }
}