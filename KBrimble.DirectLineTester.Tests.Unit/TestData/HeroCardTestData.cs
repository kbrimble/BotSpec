using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Tests.Unit.TestData
{
    public class HeroCardTestData
    {
        internal static IEnumerable<HeroCard> CreateHeroCardSetWithOneCardThatHasSetProperties(string title = default(string), string subtitle = default(string), string text = default(string), IList<CardImage> images = default(IList<CardImage>), IList<CardAction> buttons = default(IList<CardAction>), CardAction tap = default(CardAction))
        {
            var matchingCard = new HeroCard(title, subtitle, text, images, buttons, tap);
            var cards = CreateRandomHeroCards();
            cards.Add(matchingCard);
            return cards;
        }
        internal static IEnumerable<HeroCard> CreateHeroCardSetWithAllCardsWithSetProperties(string title = default(string), string subtitle = default(string), string text = default(string), IList<CardImage> images = default(IList<CardImage>), IList<CardAction> buttons = default(IList<CardAction>), CardAction tap = default(CardAction))
        {
            var cards = new List<HeroCard>();
            for (var i = 0; i < 5; i++)
            {
                var matchingCard = new HeroCard(title, subtitle, text, images, buttons, tap);
                cards.Add(matchingCard);
            }
            return cards;
        }

        internal static List<HeroCard> CreateRandomHeroCards()
        {
            var cards = new List<HeroCard>();
            for (var i = 0; i < 5; i++)
            {
                cards.Add(new HeroCard($"title{i}", $"subtitle{i}", $"text{i}"));
            }
            return cards;
        }
    }
}