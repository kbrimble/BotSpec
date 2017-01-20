using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector.DirectLine;
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

        public IEnumerable<T> ExtractCards<T>(ActivitySet activitySet) => activitySet.Activities.SelectMany(ExtractCards<T>);
        public IEnumerable<T> ExtractCards<T>(IEnumerable<Activity> activitySet) => activitySet.SelectMany(ExtractCards<T>);

        public IEnumerable<T> ExtractCards<T>(Activity activity)
        {
            if (activity.Attachments == null || !activity.Attachments.Any())
                yield break;

            var cardType = typeof(T);
            var contentType = ContentTypeMap.Map(cardType);
            if (string.IsNullOrWhiteSpace(contentType))
                throw new InvalidOperationException($"Cannot get ContentType property of type {cardType.Name}");

            var cardAttachments = activity.Attachments.Where(att => att.ContentType == contentType).ToList();

            var cardsWithContent = cardAttachments.Where(att => att.Content != null).ToList();
            var cardsWithUrls = cardAttachments.Where(att => att.ContentUrl != null && !cardsWithContent.Contains(att)).ToList();

            var jsonAttachments = new List<string>();

            if (cardsWithContent.Any())
                jsonAttachments.AddRange(cardsWithContent.Select(card => card.Content.ToString()));

            if (cardsWithUrls.Any())
            {
                var urls = cardsWithUrls.Select(tca => tca.ContentUrl).ToArray();
                jsonAttachments.AddRange(_attachmentRetriever.GetAttachmentsFromUrls(urls));
            }

            foreach (var json in jsonAttachments)
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

        internal static class ContentTypeMap
        {
            private static readonly Dictionary<Type, string> TypeMap = new Dictionary<Type, string>
            {
                [typeof(ThumbnailCard)] = "application/vnd.microsoft.card.thumbnail",
                [typeof(HeroCard)] = "application/vnd.microsoft.card.hero",
                [typeof(ReceiptCard)] = "application/vnd.microsoft.card.receipt",
                [typeof(SigninCard)] = "application/vnd.microsoft.card.signin",
                [typeof(AnimationCard)] = "application/vnd.microsoft.card.animation",
                [typeof(AudioCard)] = "application/vnd.microsoft.card.audio",
                [typeof(VideoCard)] = "application/vnd.microsoft.card.video",
            };

            public static string Map(Type t) => TypeMap[t];
        }
    }
}