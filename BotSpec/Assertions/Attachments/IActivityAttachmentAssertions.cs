using BotSpec.Assertions.Cards;

namespace BotSpec.Assertions.Attachments
{
    public interface IActivityAttachmentAssertions
    {
        IThumbnailCardAssertions OfTypeThumbnailCard();
        IHeroCardAssertions OfTypeHeroCard();
        ISigninCardAssertions OfTypeSigninCard();
        IReceiptCardAssertions OfTypeReceiptCard();
    }
}
