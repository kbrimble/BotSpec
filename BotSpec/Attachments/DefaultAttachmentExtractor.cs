using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;

namespace BotSpec.Attachments
{
    internal class DefaultAttachmentExtractor : IAttachmentExtractor
    {
        private readonly IAttachmentRetriever _attachmentRetriever;

        public DefaultAttachmentExtractor()
        {
            _attachmentRetriever = AttachmentRetrieverFactory.GetAttachmentRetriever();
        }

        public IEnumerable<T> ExtractCards<T>(MessageSet messageSet) => messageSet.Messages.SelectMany(ExtractCards<T>);
        public IEnumerable<T> ExtractCards<T>(IEnumerable<Message> messageSet) => messageSet.SelectMany(ExtractCards<T>);

        public IEnumerable<T> ExtractCards<T>(Message message)
        {
            var cardType = typeof(T);
            var contentType = cardType.GetField("ContentType")?.GetValue(null)?.ToString();
            if (string.IsNullOrWhiteSpace(contentType))
                throw new InvalidOperationException($"Cannot get ContentType property of type {cardType.Name}");

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