﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class For_a_quantity_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasQuantityMatching_should_pass_if_regex_exactly_matches_message_Quantity_of_one_item(string itemQuantityAndRegex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(quantity: itemQuantityAndRegex);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(itemQuantityAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasQuantityMatching_should_pass_if_regex_exactly_matches_Quantity_of_at_least_1_item_regardless_of_case(string itemQuantity, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(quantity: itemQuantity);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasQuantityMatching_should_pass_when_using_standard_regex_features(string itemQuantity, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(quantity: itemQuantity);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasQuantityMatching_should_throw_ReceiptItemAssertionFailedException_when_regex_matches_no_items(string regex)
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void HasQuantityMatching_should_throw_ReceiptItemAssertionFailedException_when_Quantity_of_all_items_is_null()
        {
            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(".*");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void HasQuantityMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_Quantity_of_all_items_is_null()
        {
            IList<string> matches;

            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void HasQuantityMatching_should_not_output_matches_when_regex_does_not_match_Quantity_of_any_items()
        {
            IList<string> matches = null;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasQuantityMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Quantity_of_any_item()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            sut.HasQuantityMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasQuantityMatching_should_output_matches_when_groupMatchingRegex_matches_Quantity_of_any_item()
        {
            IList<string> matches;

            const string someQuantity = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(quantity: someQuantity);

            var sut = new ReceiptItemSetAssertions(items);

            sut.HasQuantityMatching(someQuantity, $"({someQuantity})", out matches);

            matches.First().Should().Be(someQuantity);
        }

        [Test]
        public void HasQuantityMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Quantity_several_times_for_a_single_item()
        {
            IList<string> matches;

            const string someQuantity = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(quantity: someQuantity);

            var sut = new ReceiptItemSetAssertions(items);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasQuantityMatching(someQuantity, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasQuantityMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Quantity_on_multiple_items()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();
            items.Add(new ReceiptItem(quantity: "some text"));
            items.Add(new ReceiptItem(quantity: "same text"));

            var sut = new ReceiptItemSetAssertions(items);

            sut.HasQuantityMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasQuantityMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasQuantityMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasQuantityMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.HasQuantityMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
