using BotSpec.Assertions.Cards.CardComponents;

namespace BotSpec.Assertions.Cards
{
    public interface ICanAssertReceiptItems
    {
        IReceiptItemAssertions WithReceiptItem();
    }
}