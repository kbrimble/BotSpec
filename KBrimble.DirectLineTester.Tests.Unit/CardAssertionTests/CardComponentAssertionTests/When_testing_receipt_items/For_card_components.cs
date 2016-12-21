using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_items
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithTapActionThat_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var receiptItem = new ReceiptItem(tap: tap);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImageThat_should_return_CardImageAssertions()
        {
            var cardImage = new CardImage();
            var receiptItem = new ReceiptItem(image: cardImage);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.WithCardImageThat().Should().BeAssignableTo<CardImageAssertions>().And.NotBeNull();
        }

    }
}
