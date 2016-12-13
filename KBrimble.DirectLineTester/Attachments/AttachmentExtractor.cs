using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;

namespace KBrimble.DirectLineTester.Attachments
{
    public class AttachmentExtractor
    {
        private readonly IAttachmentRetreiver _attachmentRetriever;

        public AttachmentExtractor(IAttachmentRetreiver attachmentRetriever)
        {
            _attachmentRetriever = attachmentRetriever;
        }

        public IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessage(Message message)
        {
            var thumbnailCardAttachments = message.Attachments.Where(att => att.ContentType == ThumbnailCard.ContentType);
            var urls = thumbnailCardAttachments.Select(tca => tca.Url).ToArray();
            var jsonResponses = _attachmentRetriever.GetAttachmentsFromUrls(urls);
            foreach (var json in jsonResponses)
            {
                ThumbnailCard card;
                try
                {
                    card = JsonConvert.DeserializeObject<ThumbnailCard>(json);
                }
                catch (Exception)
                {
                    continue;
                }
                yield return card;
            }
        }

        public IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessageSet(MessageSet messageSet)
        {
            return ExtractThumbnailCardsFromMessageSet(messageSet.Messages);
        }

        public IEnumerable<ThumbnailCard> ExtractThumbnailCardsFromMessageSet(IEnumerable<Message> messageSet)
        {
            var cards = new List<ThumbnailCard>();
            foreach (var message in messageSet)
            {
                cards.AddRange(ExtractThumbnailCardsFromMessage(message));
            }
            return cards;
        }

        public IEnumerable<HeroCard> ExtractHeroCardsFromMessageSet(MessageSet messageSet)
        {
            return ExtractHeroCardsFromMessageSet(messageSet.Messages);
        }

        public IEnumerable<HeroCard> ExtractHeroCardsFromMessageSet(IEnumerable<Message> messageSet)
        {
            var cards = new List<HeroCard>();
            foreach (var message in messageSet)
            {
                cards.AddRange(ExtractHeroCardsFromMessage(message));
            }
            return cards;
        }

        public IEnumerable<HeroCard> ExtractHeroCardsFromMessage(Message message)
        {
            var heroCardAttachments = message.Attachments.Where(att => att.ContentType == HeroCard.ContentType);
            var urls = heroCardAttachments.Select(tca => tca.Url).ToArray();
            var jsonResponses = _attachmentRetriever.GetAttachmentsFromUrls(urls);
            foreach (var json in jsonResponses)
            {
                HeroCard card;
                try
                {
                    card = JsonConvert.DeserializeObject<HeroCard>(json);
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