using BotSpec.Assertions.Cards;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_hero_cards
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var heroCard = new HeroCard(buttons: buttons);

            var sut = new HeroCardAssertions(heroCard);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImage_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var heroCard = new HeroCard(images: cardImages);

            var sut = new HeroCardAssertions(heroCard);

            sut.WithCardImage().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var heroCard = new HeroCard(tap: tap);

            var sut = new HeroCardAssertions(heroCard);

            sut.WithTapAction().Should().BeAssignableTo<CardActionAssertions>().And.NotBeNull();
        }
    }
}
