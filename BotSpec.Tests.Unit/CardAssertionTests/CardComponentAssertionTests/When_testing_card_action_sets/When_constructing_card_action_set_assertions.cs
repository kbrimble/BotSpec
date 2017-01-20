using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_action_sets
{
    [TestFixture]
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class When_constructing_card_action_set_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_CardAction_list_is_null()
        {
            Action act = () => new CardActionSetAssertions(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Only_non_null_CardActions_should_be_available_for_assertion()
        {
            var nonNullCardActions = CardActionTestData.CreateRandomCardActions();
            var inputList = new List<CardAction> { null };
            inputList.AddRange(nonNullCardActions);

            var sut = new CardActionSetAssertions(inputList);
            sut.CardActions.ShouldBeEquivalentTo(nonNullCardActions);
        }
    }
}
