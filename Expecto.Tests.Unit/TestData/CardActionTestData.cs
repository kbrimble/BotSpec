using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Models.Cards;

namespace Expecto.Tests.Unit.TestData
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
            var types = Enum.GetValues(typeof(CardActionType)).Cast<CardActionType>().ToList();
            for (var i = 0; i < 5; i++)
            {
                var type = CardActionTypeMap.Map(types[i % types.Count]);
                cardActions.Add(new CardAction(type, $"title{i}", $"image{i}", $"value{i}"));
            }
            return cardActions;
        }
    }
}