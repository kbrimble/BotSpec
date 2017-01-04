using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Exceptions;
using Expecto.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_items
{
    [TestFixture]
    public class For_price_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void WithPriceMatching_should_pass_if_regex_exactly_matches_message_Price(string itemPriceAndRegex)
        {
            var receiptItem = new ReceiptItem(price: itemPriceAndRegex);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.PriceMatching(itemPriceAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void WithPriceMatching_should_pass_regardless_of_case(string itemPrice, string regex)
        {
            var receiptItem = new ReceiptItem(price: itemPrice);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.PriceMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void WithPriceMatching_should_pass_when_using_standard_regex_features(string itemPrice, string regex)
        {
            var receiptItem = new ReceiptItem(price: itemPrice);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.PriceMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void WithPriceMatching_should_throw_ReceiptItemAssertionFailedException_for_non_matching_regexes(string itemPrice, string regex)
        {
            var receiptItem = new ReceiptItem(price: itemPrice);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.PriceMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithPriceMatching_should_not_output_matches_when_regex_does_not_match_price()
        {
            IList<string> matches = null;

            var receiptItem = new ReceiptItem(price: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.PriceMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void WithPriceMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_price()
        {
            IList<string> matches;

            var receiptItem = new ReceiptItem(price: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.PriceMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void WithPriceMatching_should_output_matches_when_groupMatchingRegex_matches_price()
        {
            IList<string> matches;

            const string somePrice = "some text";
            var receiptItem = new ReceiptItem(price: somePrice);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.PriceMatching(somePrice, $"({somePrice})", out matches);

            matches.First().Should().Be(somePrice);
        }

        [Test]
        public void WithPriceMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_price_several_times()
        {
            IList<string> matches;

            const string somePrice = "some text";
            var receiptItem = new ReceiptItem(price: somePrice);

            var sut = new ReceiptItemAssertions(receiptItem);

            const string match1 = "some";
            const string match2 = "text";
            sut.PriceMatching(somePrice, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void WithPriceMatching_should_throw_ReceiptItemAssertionFailedException_when_price_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.PriceMatching("anything");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithPriceMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_price_is_null()
        {
            IList<string> matches;
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.PriceMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithPriceMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.PriceMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithPriceMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.PriceMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithPriceMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.PriceMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
