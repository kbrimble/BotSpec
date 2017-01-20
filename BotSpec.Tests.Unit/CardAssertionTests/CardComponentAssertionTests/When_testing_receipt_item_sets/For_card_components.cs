using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithCardImage_should_return_CardImageAssertions()
        {
            var cardImage = new CardImage();
            var receiptItems = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(image: cardImage);

            var sut = new ReceiptItemSetAssertions(receiptItems);

            sut.WithCardImage().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var receiptItems = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(tap: tap);

            var sut = new ReceiptItemSetAssertions(receiptItems);

            sut.WithTapAction().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

    }
}
