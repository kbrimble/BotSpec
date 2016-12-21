using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_receipt_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtonsThat_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithButtonsThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapActionThat_should_return_CardActionSetAssertions()
        {
            var tap = new CardAction();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(tap: tap);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithTapActionThat().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithFactThat_should_return_FactSetAssertions()
        {
            var facts = FactTestData.CreateRandomFacts();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithAllCardsWithSetProperties(facts: facts);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithFactThat().Should().BeAssignableTo<FactSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithReceiptItemThat_should_return_ReceiptItemSetAssertions()
        {
            var receiptItems = ReceiptItemTestData.CreateRandomReceiptItems();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(items: receiptItems);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithReceiptItemThat().Should().BeAssignableTo<ReceiptItemSetAssertions>().And.NotBeNull();
        }
    }
}
