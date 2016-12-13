using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_thumbnail_cards
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtonsThat_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var thumbnailCard = new ThumbnailCard(buttons: buttons);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.WithButtonsThat().Should().BeAssignableTo<CardActionSetAssertions>();
        }

        [Test]
        public void WithCardImageThat_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var thumbnailCard = new ThumbnailCard(images: cardImages);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.WithCardImageThat().Should().BeAssignableTo<CardImageSetAssertions>();
        }

        [Test]
        public void WithTapActionThat_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var thumbnailCard = new ThumbnailCard(tap: tap);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionAssertions>();
        }
    }
}
