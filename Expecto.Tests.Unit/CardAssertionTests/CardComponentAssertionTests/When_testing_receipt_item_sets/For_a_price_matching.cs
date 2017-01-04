using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Exceptions;
using Expecto.Models.Cards;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class For_a_price_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void PriceMatching_should_pass_if_regex_exactly_matches_message_Price_of_one_item(string itemPriceAndRegex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(price: itemPriceAndRegex);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(itemPriceAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void PriceMatching_should_pass_if_regex_exactly_matches_Price_of_at_least_1_item_regardless_of_case(string itemPrice, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(price: itemPrice);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void PriceMatching_should_pass_when_using_standard_regex_features(string itemPrice, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(price: itemPrice);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void PriceMatching_should_throw_ReceiptItemAssertionFailedException_when_regex_matches_no_items(string regex)
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void PriceMatching_should_throw_ReceiptItemAssertionFailedException_when_Price_of_all_items_is_null()
        {
            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(".*");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void PriceMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_Price_of_all_items_is_null()
        {
            IList<string> matches;

            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void PriceMatching_should_not_output_matches_when_regex_does_not_match_Price_of_any_items()
        {
            IList<string> matches = null;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void PriceMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Price_of_any_item()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            sut.PriceMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void PriceMatching_should_output_matches_when_groupMatchingRegex_matches_Price_of_any_item()
        {
            IList<string> matches;

            const string somePrice = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(price: somePrice);

            var sut = new ReceiptItemSetAssertions(items);

            sut.PriceMatching(somePrice, $"({somePrice})", out matches);

            matches.First().Should().Be(somePrice);
        }

        [Test]
        public void PriceMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Price_several_times_for_a_single_item()
        {
            IList<string> matches;

            const string somePrice = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(price: somePrice);

            var sut = new ReceiptItemSetAssertions(items);

            const string match1 = "some";
            const string match2 = "text";
            sut.PriceMatching(somePrice, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void PriceMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Price_on_multiple_items()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();
            items.Add(new ReceiptItem(price: "some text"));
            items.Add(new ReceiptItem(price: "same text"));

            var sut = new ReceiptItemSetAssertions(items);

            sut.PriceMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void PriceMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void PriceMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void PriceMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.PriceMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
