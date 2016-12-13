using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_hero_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtonsThat_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var thumbnailCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new HeroCardSetAssertions(thumbnailCards);

            sut.WithButtonsThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImageThat_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var thumbnailCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(images: cardImages);

            var sut = new HeroCardSetAssertions(thumbnailCards);

            sut.WithCardImageThat().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapActionThat_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var thumbnailCards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(tap: tap);

            var sut = new HeroCardSetAssertions(thumbnailCards);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }
    }

}
