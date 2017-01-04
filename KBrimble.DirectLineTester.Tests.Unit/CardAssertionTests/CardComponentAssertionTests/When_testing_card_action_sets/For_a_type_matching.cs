using System;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_action_sets
{
    [TestFixture]
    public class For_a_type_matching
    {
        [Test]
        public void Type_should_throw_ArgumentNullException_when_type_is_null()
        {
            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.ActionType(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TypeMatching_should_throw_CardActionAssertionSetFailedException_when_no_card_action_matches()
        {
            var cardActions = CardActionTestData.CreateCardActionSetWithAllActionsWithSetProperties(type: CardActionType.Call.Value);

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.ActionType(CardActionType.DownloadFile);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TypeMatching_should_throw_CardActionAssertionFailedException_when_type_of_all_card_actions_is_null()
        {
            var cardActions = CardActionTestData.CreateCardActionSetWithAllActionsWithSetProperties(type: null);

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.ActionType(CardActionType.Call);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TypeMatching_should_not_throw_when_card_action_type_matches_expected_type()
        {
            var cardActionType = CardActionType.Call;
            var cardActions = CardActionTestData.CreateCardActionSetWithOneActionThatHasSetProperties(type: cardActionType.Value);

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.ActionType(cardActionType);

            act.ShouldNotThrow<Exception>();
        }
    }
}
