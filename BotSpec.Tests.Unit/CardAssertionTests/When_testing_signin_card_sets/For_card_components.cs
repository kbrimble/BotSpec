﻿using BotSpec.Assertions.Cards;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_signin_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var signinCards = SigninCardTestData.CreateSigninCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new SigninCardSetAssertions(signinCards);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }
    }

}
