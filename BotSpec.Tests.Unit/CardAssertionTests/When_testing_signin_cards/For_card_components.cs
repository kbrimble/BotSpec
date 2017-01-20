using BotSpec.Assertions.Cards;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_signin_cards
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var signinCard = new SigninCard(buttons: buttons);

            var sut = new SigninCardAssertions(signinCard);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }
    }
}
