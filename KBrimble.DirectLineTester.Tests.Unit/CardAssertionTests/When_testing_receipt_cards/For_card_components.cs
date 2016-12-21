using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_receipt_cards
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtonsThat_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var receiptCard = new ReceiptCard(buttons: buttons);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithButtonsThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapActionThat_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var receiptCard = new ReceiptCard(tap: tap);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithFactThat_should_return_FactSetAssertions()
        {
            var facts = FactTestData.CreateRandomFacts();
            var receiptCard = new ReceiptCard(facts: facts);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithFactThat().Should().BeAssignableTo<FactSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithReceiptItemThat_should_return_ReceiptItemSetAssertions()
        {
            var receiptItems = ReceiptItemTestData.CreateRandomReceiptItems();
            var receiptCard = new ReceiptCard(items: receiptItems);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithReceiptItemThat().Should().BeAssignableTo<ReceiptItemSetAssertions>().And.NotBeNull();
        }
    }
}
