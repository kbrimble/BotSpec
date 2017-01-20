using BotSpec.Assertions.Cards;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_hero_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var heroCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new HeroCardSetAssertions(heroCards);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImage_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var heroCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(images: cardImages);

            var sut = new HeroCardSetAssertions(heroCards);

            sut.WithCardImage().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var heroCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(tap: tap);

            var sut = new HeroCardSetAssertions(heroCards);

            sut.WithTapAction().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }
    }

}
