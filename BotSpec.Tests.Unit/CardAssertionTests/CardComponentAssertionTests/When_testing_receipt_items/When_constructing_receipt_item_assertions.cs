using System;
using BotSpec.Assertions.Cards.CardComponents;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_items
{
    [TestFixture]
    public class When_constructing_receipt_item_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_ReceiptItem_is_null()
        {
            Action act = () => new ReceiptItemAssertions(null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
