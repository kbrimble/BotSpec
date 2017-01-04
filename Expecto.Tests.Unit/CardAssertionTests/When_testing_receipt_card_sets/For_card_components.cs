using Expecto.Assertions.Cards;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.When_testing_receipt_card_sets
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(buttons: buttons);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionSetAssertions()
        {
            var tap = new CardAction();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(tap: tap);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithTapAction().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithFact_should_return_FactSetAssertions()
        {
            var facts = FactTestData.CreateRandomFacts();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithAllCardsWithSetProperties(facts: facts);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithFact().Should().BeAssignableTo<FactSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithReceiptItem_should_return_ReceiptItemSetAssertions()
        {
            var receiptItems = ReceiptItemTestData.CreateRandomReceiptItems();
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(items: receiptItems);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.WithReceiptItem().Should().BeAssignableTo<ReceiptItemSetAssertions>().And.NotBeNull();
        }
    }
}
