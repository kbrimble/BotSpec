using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;

namespace KBrimble.DirectLineTester.Attachments
{
    public class DefaultAttachmentExtractor : IAttachmentExtractor
    {
        private readonly IAttachmentRetriever _attachmentRetriever;

        public DefaultAttachmentExtractor()
        {
            _attachmentRetriever = AttachmentRetrieverFactory.GetAttachmentRetriever();
        }

        public IEnumerable<T> ExtractCardsFromMessageSet<T>(MessageSet messageSet) => messageSet.Messages.SelectMany(ExtractCardsFromMessage<T>);
        public IEnumerable<T> ExtractCardsFromMessageSet<T>(IEnumerable<Message> messageSet) => messageSet.SelectMany(ExtractCardsFromMessage<T>);

        public IEnumerable<T> ExtractCardsFromMessage<T>(Message message)
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