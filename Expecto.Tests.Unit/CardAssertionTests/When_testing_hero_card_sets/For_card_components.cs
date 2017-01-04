using Expecto.Assertions.Cards;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.When_testing_hero_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var thumbnailCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new HeroCardSetAssertions(thumbnailCards);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImage_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var thumbnailCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(images: cardImages);

            var sut = new HeroCardSetAssertions(thumbnailCards);

            sut.WithCardImage().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var thumbnailCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(tap: tap);

            var sut = new HeroCardSetAssertions(thumbnailCards);

            sut.WithTapAction().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }
    }

}
