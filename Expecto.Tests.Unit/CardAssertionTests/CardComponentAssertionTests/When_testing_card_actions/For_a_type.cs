using System;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Exceptions;
using Expecto.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_actions
{
    [TestFixture]
    public class For_a_type
    {

        [Test]
        public void TypeMatching_should_throw_CardActionAssertionFailedException_for_non_matching_types()
        {
            var type = CardActionTypeMap.Map(CardActionType.Call);
            var cardAction = new CardAction(type: type);

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
            const CardActionType call = CardActionType.Call;
            var type = CardActionTypeMap.Map(call);
            var cardAction = new CardAction(type: type);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ActionType(call);

            act.ShouldNotThrow<Exception>();
        }
    }
}