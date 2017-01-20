using System;
using System.Collections.Generic;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class When_constructing_receipt_item_set_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_ReceiptItem_list_is_null()
        {
            Action act = () => new ReceiptItemSetAssertions((IEnumerable<ReceiptItem>) null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_ReceiptCard_list_is_null()
        {
            Action act = () => new ReceiptItemSetAssertions((ReceiptCard) null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Only_non_null_ReceiptItems_from_ReceiptItem_list_should_be_available_for_assertion()
        {
            var nonNullReceiptItems = ReceiptItemTestData.CreateRandomReceiptItems();
            var receiptItems = new List<ReceiptItem> { null };
            receiptItems.AddRange(nonNullReceiptItems);

            var sut = new ReceiptItemSetAssertions(receiptItems);
            sut.ReceiptItems.ShouldBeEquivalentTo(nonNullReceiptItems);
        }

        [Test]
        public void Only_non_null_ReceiptItems_from_ReceiptCard_should_be_available_for_assertion()
        {
            var nonNullReceiptItems = ReceiptItemTestData.CreateRandomReceiptItems();
            var receiptItems = new List<ReceiptItem> { null };
            receiptItems.AddRange(nonNullReceiptItems);
            var receiptCard = new ReceiptCard(items: receiptItems);

            var sut = new ReceiptItemSetAssertions(receiptCard);
            sut.ReceiptItems.ShouldBeEquivalentTo(nonNullReceiptItems);

        }
    }
}
