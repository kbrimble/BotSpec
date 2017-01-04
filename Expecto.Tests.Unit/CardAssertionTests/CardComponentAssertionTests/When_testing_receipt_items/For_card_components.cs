﻿using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_items
{
    [TestFixture]
    public class For_card_components
    {
        [Test]
        public void WithTapAction_should_return_CardActionAssertions()
        {
            var tap = new CardAction();
            var receiptItem = new ReceiptItem(tap: tap);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.WithTapAction().Should().BeAssignableTo<CardActionAssertions>().And.NotBeNull();
        }

        [Test]
        public void WithCardImage_should_return_CardImageAssertions()
        {
            var cardImage = new CardImage();
            var receiptItem = new ReceiptItem(image: cardImage);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.WithCardImage().Should().BeAssignableTo<CardImageAssertions>().And.NotBeNull();
        }

    }
}