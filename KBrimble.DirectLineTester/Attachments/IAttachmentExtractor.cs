using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Attachments
{
    public interface IAttachmentExtractor
    {
        IEnumerable<HeroCard> ExtractHeroCardsFromMessage(Message message);
        IEnumerable<HeroCard> ExtractHeroCardsFromMessageSet(MessageSet messageSet);
        IEnumerable<HeroCard> ExtractHeroCardsFromMessageSet(IEnumerable<Message> messageSet);
        IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessage(Message message);
        IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessageSet(MessageSet messageSet);
        IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessageSet(IEnumerable<Message> messageSet);
        IEnumerable<ReceiptCard> ExtractReceiptCardsFromMessage(Message message);
        IEnumerable<ReceiptCard> ExtractReceiptCardsFromMessageSet(IEnumerable<Message> messageSet);
        IEnumerable<ReceiptCard> ExtractReceiptCardsFromMessageSet(MessageSet messageSet);
        IEnumerable<SigninCard> ExtractSigninCardsFromMessage(Message message);
        IEnumerable<SigninCard> ExtractSigninCardsFromMessageSet(IEnumerable<Message> messageSet);
        IEnumerable<SigninCard> ExtractSigninCardsFromMessageSet(MessageSet messageSet);
    }
}