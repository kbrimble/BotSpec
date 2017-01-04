using System.Collections.Generic;

namespace Expecto.Models.Cards
{
    internal interface IHaveReceiptItems
    {
        IList<ReceiptItem> Items { get; set; }
    }
}