using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_thumbnail_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtonsThat_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var thumbnailCards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new ThumbnailCardSetAssertions(thumbnailCards);

            sut.WithButtonsThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImageThat_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var thumbnailCards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(images: cardImages);

            var sut = new ThumbnailCardSetAssertions(thumbnailCards);

            sut.WithCardImageThat().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapActionThat_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var thumbnailCards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(tap: tap);

            var sut = new ThumbnailCardSetAssertions(thumbnailCards);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }
    }

}
