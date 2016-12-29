using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Tests.Unit.TestData
{
    public class SigninCardTestData
    {
        internal static IEnumerable<SigninCard> CreateSigninCardSetWithOneCardThatHasSetProperties(string text = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            var matchingCard = new SigninCard(text, buttons);
            var cards = CreateRandomSigninCards();
            cards.Add(matchingCard);
            return cards;
        }
        internal static IEnumerable<SigninCard> CreateSigninCardSetWithAllCardsWithSetProperties(string text = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            var cards = new List<SigninCard>();
            for (var i = 0; i < 5; i++)
            {
                var matchingCard = new SigninCard(text, buttons);
                cards.Add(matchingCard);
            }
            return cards;
        }

        internal static List<SigninCard> CreateRandomSigninCards()
        {
            var cards = new List<SigninCard>();
            for (var i = 0; i < 5; i++)
            {
                cards.Add(new SigninCard($"text{i}"));
            }
            return cards;
        }
    }
}