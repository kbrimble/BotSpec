﻿using Expecto.Assertions.Cards;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.When_testing_receipt_cards
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithButtons_should_return_CardActionSetAssertions()
        {
            var buttons = CardActionTestData.CreateRandomCardActions();
            var receiptCard = new ReceiptCard(buttons: buttons);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithButtons().Should().BeAssignableTo<CardActionSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var receiptCard = new ReceiptCard(tap: tap);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithTapAction().Should().BeAssignableTo<CardActionAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithFact_should_return_FactSetAssertions()
        {
            var facts = FactTestData.CreateRandomFacts();
            var receiptCard = new ReceiptCard(facts: facts);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithFact().Should().BeAssignableTo<FactSetAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithReceiptItem_should_return_ReceiptItemSetAssertions()
        {
            var receiptItems = ReceiptItemTestData.CreateRandomReceiptItems();
            var receiptCard = new ReceiptCard(items: receiptItems);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.WithReceiptItem().Should().BeAssignableTo<ReceiptItemSetAssertions>().And.NotBeNull();
        }
    }
}