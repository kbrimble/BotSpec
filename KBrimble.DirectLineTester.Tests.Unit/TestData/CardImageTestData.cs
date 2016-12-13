using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Tests.Unit.TestData
{
    public class CardImageTestData
    {
        internal static IList<CardImage> CreateCardImageSetWithOneImageThatHasSetProperties(string url = default(string), string alt = default(string), CardAction tap = default(CardAction))
        {
            var matchingCardImage = new CardImage(url, alt, tap);
            var cards = CreateRandomCardImages();
            cards.Add(matchingCardImage);
            return cards;
        }

        internal static IList<CardImage> CreateCardImageSetWithAllImagesWithSetProperties(string url = default(string), string alt = default(string), CardAction tap = default(CardAction))
        {
            var cards = new List<CardImage>();
            for (var i = 0; i < 5; i++)
            {
                var matchingCardImage = new CardImage(url, alt, tap);
                cards.Add(matchingCardImage);
            }
            return cards;
        }

        internal static List<CardImage> CreateRandomCardImages()
        {
            var cardImages = new List<CardImage>();
            for (var i = 0; i < 5; i++)
            {
                cardImages.Add(new CardImage($"url{i}", $"alt{i}"));
            }
            return cardImages;
        }
    }
}