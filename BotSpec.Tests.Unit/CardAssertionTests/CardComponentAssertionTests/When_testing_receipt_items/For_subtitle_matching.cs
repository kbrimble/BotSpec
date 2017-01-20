using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_items
{
    [TestFixture]
    public class For_subsubtitle_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void WithSubtitleMatching_should_pass_if_regex_exactly_matches_message_Subtitle(string itemSubtitleAndRegex)
        {
            var receiptItem = new ReceiptItem(subtitle: itemSubtitleAndRegex);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.SubtitleMatching(itemSubtitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void WithSubtitleMatching_should_pass_regardless_of_case(string itemSubtitle, string regex)
        {
            var receiptItem = new ReceiptItem(subtitle: itemSubtitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void WithSubtitleMatching_should_pass_when_using_standard_regex_features(string itemSubtitle, string regex)
        {
            var receiptItem = new ReceiptItem(subtitle: itemSubtitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void WithSubtitleMatching_should_throw_ReceiptItemAssertionFailedException_for_non_matching_regexes(string itemSubtitle, string regex)
        {
            var receiptItem = new ReceiptItem(subtitle: itemSubtitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithSubtitleMatching_should_not_output_matches_when_regex_does_not_match_subtitle()
        {
            IList<string> matches = null;

            var receiptItem = new ReceiptItem(subtitle: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            Action act = () => sut.SubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void WithSubtitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_subtitle()
        {
            IList<string> matches;

            var receiptItem = new ReceiptItem(subtitle: "some text");

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.SubtitleMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void WithSubtitleMatching_should_output_matches_when_groupMatchingRegex_matches_subtitle()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var receiptItem = new ReceiptItem(subtitle: someSubtitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            sut.SubtitleMatching(someSubtitle, $"({someSubtitle})", out matches);

            matches.First().Should().Be(someSubtitle);
        }

        [Test]
        public void WithSubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_subtitle_several_times()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var receiptItem = new ReceiptItem(subtitle: someSubtitle);

            var sut = new ReceiptItemAssertions(receiptItem);

            const string match1 = "some";
            const string match2 = "text";
            sut.SubtitleMatching(someSubtitle, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void WithSubtitleMatching_should_throw_ReceiptItemAssertionFailedException_when_subtitle_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.SubtitleMatching("anything");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithSubtitleMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_subtitle_is_null()
        {
            IList<string> matches;
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.SubtitleMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void WithSubtitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.SubtitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithSubtitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.SubtitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WithSubtitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var item = new ReceiptItem();

            var sut = new ReceiptItemAssertions(item);

            Action act = () => sut.SubtitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
