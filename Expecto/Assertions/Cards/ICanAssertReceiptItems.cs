using Expecto.Assertions.Cards.CardComponents;

namespace Expecto.Assertions.Cards
{
    public interface ICanAssertReceiptItems
    {
        IReceiptItemAssertions WithReceiptItem();
    }
}