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
    public class For_text_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void WithTextMatching_should_pass_if_regex_exactly_matches_message_Text(string itemTextAndRegex)
        {
            var receiptItem = new ReceiptItem(text: itemTextAndRegex);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.TextMatching(itemTextAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void WithTextMatching_should_pass_regardless_of_case(string itemText, string regex)
        {
            var receiptItem = new ReceiptItem(text: itemText);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void WithTextMatching_should_pass_when_using_standard_regex_features(string itemText, string regex)
        {
            var receiptItem = new ReceiptItem(text: itemText);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void WithTextMatching_should_throw_ReceiptItemAssertionFailedException_for_non_matching_regexes(string itemText, string regex)
        {
            var receiptItem = new ReceiptItem(text: itemText);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.TextMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithTextMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var receiptItem = new ReceiptItem(text: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.TextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void WithTextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var receiptItem = new ReceiptItem(text: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.TextMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void WithTextMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var receiptItem = new ReceiptItem(text: someText);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.TextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void WithTextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var receiptItem = new ReceiptItem(text: someText);

            var sut = new ReceiptItemAssertions(receiptItem);

            const string match1 = "some";
            const string match2 = "text";
            sut.TextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void WithTextMatching_should_throw_ReceiptItemAssertionFailedException_when_text_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.TextMatching("anything");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithTextMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.TextMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithTextMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.TextMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithTextMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.TextMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithTextMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.TextMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
