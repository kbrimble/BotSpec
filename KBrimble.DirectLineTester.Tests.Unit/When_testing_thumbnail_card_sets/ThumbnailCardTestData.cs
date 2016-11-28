using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Tests.Unit.When_testing_thumbnail_card_sets
{
    public class ThumbnailCardTestData
    {
        internal static IEnumerable<ThumbnailCard> CreateThumbnailCardSetWithOneMessageThatHasSetProperties(string title = default(string), string subtitle = default(string), string text = default(string), IList<CardImage> images = default(IList<CardImage>), IList<CardAction> buttons = default(IList<CardAction>), CardAction tap = default(CardAction))
        {
            var matchingCard = new ThumbnailCard(title, subtitle, text, images, buttons, tap);
            var cards = CreateRandomThumbnailCards();
            cards.Add(matchingCard);
            return cards;
        }

        internal static List<ThumbnailCard> CreateRandomThumbnailCards()
        {
            var cards = new List<ThumbnailCard>();
            for (var i = 0; i < 5; i++)
            {
                cards.Add(new ThumbnailCard($"title{i}", $"subtitle{i}", $"text{i}"));
            }
            return cards;
        }
    }
}