using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_action_sets
{
    [TestFixture]
    public class When_constructing_card_action_set_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_CardAction_list_is_null()
        {
            Action act = () => new CardActionSetAssertions((IList<CardAction>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_IHaveButtons_list_is_null()
        {
            Action act = () => new CardActionSetAssertions((IEnumerable<IHaveButtons>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_IHaveTapAction_list_is_null()
        {
            Action act = () => new CardActionSetAssertions((IEnumerable<IHaveTapAction>)null);
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

        [Test]
        public void Only_non_null_IHaveButtonses_should_be_available_for_assertion()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            IEnumerable<IHaveButtons> nonNullIHaveButtonses = ThumbnailCardTestData.CreateThumbnailCardSetWithAllCardsWithSetProperties(buttons: buttons);
            var cardActions = new List<IHaveButtons> { null };
            cardActions.AddRange(nonNullIHaveButtonses);
            var inputList = cardActions.Cast<IHaveButtons>();

            var sut = new CardActionSetAssertions(inputList);
            sut.CardActions.ShouldBeEquivalentTo(nonNullIHaveButtonses.SelectMany(x => x.Buttons));
        }

        [Test]
        public void Only_non_null_IHaveTapActions_should_be_available_for_assertion()
        {
            var tap = new CardAction();
            IEnumerable<IHaveTapAction> nonNullIHaveTapActions = ThumbnailCardTestData.CreateThumbnailCardSetWithAllCardsWithSetProperties(tap: tap);
            var cardActions = new List<IHaveTapAction> { null };
            cardActions.AddRange(nonNullIHaveTapActions);
            var inputList = cardActions.Cast<IHaveTapAction>();

            var sut = new CardActionSetAssertions(inputList);
            sut.CardActions.ShouldBeEquivalentTo(nonNullIHaveTapActions.Select(x => x?.Tap));
        }
    }
}
