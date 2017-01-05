using BotSpec.Assertions.Cards;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Models.Cards;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_thumbnail_cards
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var thumbnailCard = new ThumbnailCard(buttons: buttons);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImage_should_return_CardImageAssertions()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();
            var thumbnailCard = new ThumbnailCard(images: cardImages);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.WithCardImage().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var thumbnailCard = new ThumbnailCard(tap: tap);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.WithTapAction().Should().BeAssignableTo<CardActionAssertions>().And.NotBeNull();
        }
    }
}
