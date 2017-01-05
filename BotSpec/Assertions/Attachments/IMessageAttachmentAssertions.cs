using BotSpec.Assertions.Cards;

namespace BotSpec.Assertions.Attachments
{
    public interface IMessageAttachmentAssertions
    {
        IThumbnailCardAssertions OfTypeThumbnailCard();
        IHeroCardAssertions OfTypeHeroCard();
        ISigninCardAssertions OfTypeSigninCard();
        IReceiptCardAssertions OfTypeReceiptCard();
    }
}
