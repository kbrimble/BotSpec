using KBrimble.DirectLineTester.Assertions.Cards;

namespace KBrimble.DirectLineTester.Assertions.Attachments
{
    public interface IMessageAttachmentAssertions
    {
        IThumbnailCardAssertions OfTypeThumbnailCardThat();
    }
}
