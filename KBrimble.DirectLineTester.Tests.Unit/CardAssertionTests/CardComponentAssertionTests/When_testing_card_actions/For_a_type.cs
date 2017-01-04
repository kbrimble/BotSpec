using System;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_actions
{
    [TestFixture]
    public class For_a_type
    {
        [Test]
        public void Type_should_throw_ArgumentNullException_when_type_is_null()
        {
            var cardAction = new CardAction(type: CardActionType.Call.Value);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ActionType(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Type_should_throw_CardActionAssertionFailedException_when_card_action_type_has_an_invalid_value()
        {
            var cardAction = new CardAction(type: "invalidType");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ActionType(CardActionType.Call);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TypeMatching_should_throw_CardActionAssertionFailedException_for_non_matching_types()
        {
            var cardAction = new CardAction(type: CardActionType.Call.Value);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ActionType(CardActionType.DownloadFile);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TypeMatching_should_throw_CardActionAssertionFailedException_when_type_of_card_action_is_null()
        {
            var cardAction = new CardAction(type: null);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ActionType(CardActionType.Call);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TypeMatching_should_not_throw_when_card_action_type_matches_expected_type()
        {
            var cardActionType = CardActionType.Call;
            var cardAction = new CardAction(type: cardActionType.Value);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ActionType(cardActionType);

            act.ShouldNotThrow<Exception>();
        }
    }
}