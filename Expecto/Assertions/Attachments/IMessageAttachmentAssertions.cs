using Expecto.Assertions.Cards;

namespace Expecto.Assertions.Attachments
{
    public interface IMessageAttachmentAssertions
    {
        IThumbnailCardAssertions OfTypeThumbnailCard();
        IHeroCardAssertions OfTypeHeroCard();
        ISigninCardAssertions OfTypeSigninCard();
        IReceiptCardAssertions OfTypeReceiptCard();
    }
}
