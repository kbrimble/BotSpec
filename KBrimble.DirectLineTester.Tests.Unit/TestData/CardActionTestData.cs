using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Tests.Unit.TestData
{
    public class CardActionTestData
    {
        internal static IList<CardAction> CreateCardActionSetWithOneActionThatHasSetProperties(string type = default(string), string title = default(string), string image = default(string), string value = default(string))
        {
            var matchingCardAction = new CardAction(type, title, image, value);
            var cards = CreateRandomCardActions();
            cards.Add(matchingCardAction);
            return cards;
        }

        internal static IList<CardAction> CreateCardActionSetWithAllActionsWithSetProperties(string type = default(string), string title = default(string), string image = default(string), string value = default(string))
        {
            var cards = new List<CardAction>();
            for (var i = 0; i < 5; i++)
            {
                var matchingCardAction = new CardAction(type, title, image, value);
                cards.Add(matchingCardAction);
            }
            return cards;
        }

        internal static List<CardAction> CreateRandomCardActions()
        {
            var cardActions = new List<CardAction>();
            for (var i = 0; i < 5; i++)
            {
                var type = CardActionType.AllTypes[i % CardActionType.AllTypes.Length];
                cardActions.Add(new CardAction(type.Value, $"title{i}", $"image{i}", $"value{i}"));
            }
            return cardActions;
        }
    }
}