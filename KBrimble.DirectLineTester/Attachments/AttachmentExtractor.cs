using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;

namespace KBrimble.DirectLineTester.Attachments
{
    public class AttachmentExtractor : IAttachmentExtractor
    {
        private readonly IAttachmentRetriever _attachmentRetriever;

        public AttachmentExtractor()
        {
            _attachmentRetriever = AttachmentRetrieverFactory.GetAttachmentRetriever();
        }

        public IEnumerable<HeroCard> ExtractHeroCardsFromMessage(Message message) => ExtractCardsFromMessage<HeroCard>(message);

        public IEnumerable<HeroCard> ExtractHeroCardsFromMessageSet(MessageSet messageSet) => ExtractHeroCardsFromMessageSet(messageSet.Messages);

        public IEnumerable<HeroCard> ExtractHeroCardsFromMessageSet(IEnumerable<Message> messageSet) => messageSet.SelectMany(ExtractHeroCardsFromMessage);

        public IEnumerable<ReceiptCard> ExtractReceiptCardsFromMessage(Message message) => ExtractCardsFromMessage<ReceiptCard>(message);

        public IEnumerable<ReceiptCard> ExtractReceiptCardsFromMessageSet(IEnumerable<Message> messageSet) => messageSet.SelectMany(ExtractReceiptCardsFromMessage);

        public IEnumerable<ReceiptCard> ExtractReceiptCardsFromMessageSet(MessageSet messageSet) => ExtractReceiptCardsFromMessageSet(messageSet.Messages);

        public IEnumerable<SigninCard> ExtractSigninCardsFromMessage(Message message) => ExtractCardsFromMessage<SigninCard>(message);

        public IEnumerable<SigninCard> ExtractSigninCardsFromMessageSet(IEnumerable<Message> messageSet) => messageSet.SelectMany(ExtractSigninCardsFromMessage);

        public IEnumerable<SigninCard> ExtractSigninCardsFromMessageSet(MessageSet messageSet) => ExtractSigninCardsFromMessageSet(messageSet.Messages);

        public IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessage(Message message) => ExtractCardsFromMessage<ThumbnailCard>(message);

        public IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessageSet(MessageSet messageSet) => ExtractThumbnailCardsFromMessageSet(messageSet.Messages);

        public IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessageSet(IEnumerable<Message> messageSet) => messageSet.SelectMany(ExtractThumbnailCardsFromMessage);

        private IEnumerable<T> ExtractCardsFromMessage<T>(Message message)
        {
            var contentType = typeof(T).GetField("ContentType").GetValue(null)?.ToString();
            if (string.IsNullOrWhiteSpace(contentType))
                throw new InvalidOperationException($"Cannot get ContentType property of type {typeof(T).Name}");

            var cardAttachments = message.Attachments.Where(att => att.ContentType == contentType);
            var urls = cardAttachments.Select(tca => tca.Url).ToArray();
            var jsonResponses = _attachmentRetriever.GetAttachmentsFromUrls(urls);
            foreach (var json in jsonResponses)
            {
                T card;
                try
                {
                    card = JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception)
                {
                    continue;
                }
                yield return card;
            }

        }
    }
}