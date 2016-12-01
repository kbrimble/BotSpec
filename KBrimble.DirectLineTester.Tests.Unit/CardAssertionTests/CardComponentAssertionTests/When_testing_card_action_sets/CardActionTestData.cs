using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_action_sets
{
    public class CardActionTestData
    {
        internal static IList<CardAction> CreateCardActionSetWithOneMessageThatHasSetProperties(string type = default(string), string title = default(string), string image = default(string), string value = default(string))
        {
            var matchingCardAction = new CardAction(type, title, image, value);
            var cards = CreateRandomCardActions();
            cards.Add(matchingCardAction);
            return cards;
        }

        internal static List<CardAction> CreateRandomCardActions()
        {
            var cardActions = new List<CardAction>();
            for (var i = 0; i < 5; i++)
            {
                cardActions.Add(new CardAction($"type{i}", $"title{i}", $"image{i}", $"value{i}"));
            }
            return cardActions;
        }
    }
}