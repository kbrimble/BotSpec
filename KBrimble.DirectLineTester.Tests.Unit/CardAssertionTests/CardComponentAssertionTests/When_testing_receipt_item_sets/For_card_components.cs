using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithCardImageThat_should_return_CardImageAssertions()
        {
            var cardImage = new CardImage();
            var receiptItems = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(image: cardImage);

            var sut = new ReceiptItemSetAssertions(receiptItems);

            sut.WithCardImageThat().Should().BeAssignableTo<CardImageSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapActionThat_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var receiptItems = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(tap: tap);

            var sut = new ReceiptItemSetAssertions(receiptItems);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

    }
}
