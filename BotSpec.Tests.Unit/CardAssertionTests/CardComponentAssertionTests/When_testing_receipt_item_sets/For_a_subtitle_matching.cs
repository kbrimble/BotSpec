using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class For_a_subtitle_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void SubtitleMatching_should_pass_if_regex_exactly_matches_message_Subtitle_of_one_item(string itemSubtitleAndRegex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(subtitle: itemSubtitleAndRegex);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(itemSubtitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void SubtitleMatching_should_pass_if_regex_exactly_matches_Subtitle_of_at_least_1_item_regardless_of_case(string itemSubtitle, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(subtitle: itemSubtitle);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void SubtitleMatching_should_pass_when_using_standard_regex_features(string itemSubtitle, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(subtitle: itemSubtitle);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void SubtitleMatching_should_throw_ReceiptItemAssertionFailedException_when_regex_matches_no_items(string regex)
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ReceiptItemAssertionFailedException_when_Subtitle_of_all_items_is_null()
        {
            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(".*");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_Subtitle_of_all_items_is_null()
        {
            IList<string> matches;

            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_not_output_matches_when_regex_does_not_match_Subtitle_of_any_items()
        {
            IList<string> matches = null;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void SubtitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Subtitle_of_any_item()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            sut.SubtitleMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void SubtitleMatching_should_output_matches_when_groupMatchingRegex_matches_Subtitle_of_any_item()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(subtitle: someSubtitle);

            var sut = new ReceiptItemSetAssertions(items);

            sut.SubtitleMatching(someSubtitle, $"({someSubtitle})", out matches);

            matches.First().Should().Be(someSubtitle);
        }

        [Test]
        public void SubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_several_times_for_a_single_item()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(subtitle: someSubtitle);

            var sut = new ReceiptItemSetAssertions(items);

            const string match1 = "some";
            const string match2 = "text";
            sut.SubtitleMatching(someSubtitle, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void SubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_on_multiple_items()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();
            items.Add(new ReceiptItem(subtitle: "some text"));
            items.Add(new ReceiptItem(subtitle: "same text"));

            var sut = new ReceiptItemSetAssertions(items);

            sut.SubtitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.SubtitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
