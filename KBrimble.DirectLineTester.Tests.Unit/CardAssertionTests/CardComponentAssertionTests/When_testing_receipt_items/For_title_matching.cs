using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_items
{
    [TestFixture]
    public class For_title_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void WithTitleMatching_should_pass_if_regex_exactly_matches_message_Title(string itemTitleAndRegex)
        {
            var receiptItem = new ReceiptItem(title: itemTitleAndRegex);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.HasTitleMatching(itemTitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void WithTitleMatching_should_pass_regardless_of_case(string itemTitle, string regex)
        {
            var receiptItem = new ReceiptItem(title: itemTitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void WithTitleMatching_should_pass_when_using_standard_regex_features(string itemTitle, string regex)
        {
            var receiptItem = new ReceiptItem(title: itemTitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void WithTitleMatching_should_throw_ReceiptItemAssertionFailedException_for_non_matching_regexes(string itemTitle, string regex)
        {
            var receiptItem = new ReceiptItem(title: itemTitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithTitleMatching_should_not_output_matches_when_regex_does_not_match_title()
        {
            IList<string> matches = null;

            var receiptItem = new ReceiptItem(title: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.HasTitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void WithTitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_title()
        {
            IList<string> matches;

            var receiptItem = new ReceiptItem(title: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.HasTitleMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void WithTitleMatching_should_output_matches_when_groupMatchingRegex_matches_title()
        {
            IList<string> matches;

            const string someTitle = "some text";
            var receiptItem = new ReceiptItem(title: someTitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.HasTitleMatching(someTitle, $"({someTitle})", out matches);

            matches.First().Should().Be(someTitle);
        }

        [Test]
        public void WithTitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_title_several_times()
        {
            IList<string> matches;

            const string someTitle = "some text";
            var receiptItem = new ReceiptItem(title: someTitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasTitleMatching(someTitle, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void WithTitleMatching_should_throw_ReceiptItemAssertionFailedException_when_title_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.HasTitleMatching("anything");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithTitleMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_title_is_null()
        {
            IList<string> matches;
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.HasTitleMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithTitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.HasTitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithTitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.HasTitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithTitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.HasTitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
