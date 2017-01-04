using KBrimble.DirectLineTester.Assertions.Cards;

namespace KBrimble.DirectLineTester.Assertions.Attachments
{
    public interface IMessageAttachmentAssertions
    {
        IThumbnailCardAssertions OfTypeThumbnailCard();
        IHeroCardAssertions OfTypeHeroCard();
        ISigninCardAssertions OfTypeSigninCard();
        IReceiptCardAssertions OfTypeReceiptCard();
    }
}
