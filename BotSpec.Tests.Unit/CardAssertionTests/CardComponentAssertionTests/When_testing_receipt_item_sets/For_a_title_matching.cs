using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_receipt_item_sets
{
    [TestFixture]
    public class For_a_title_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TitleMatching_should_pass_if_regex_exactly_matches_message_Title_of_one_item(string itemTitleAndRegex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(title: itemTitleAndRegex);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(itemTitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void TitleMatching_should_pass_if_regex_exactly_matches_Title_of_at_least_1_item_regardless_of_case(string itemTitle, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(title: itemTitle);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TitleMatching_should_pass_when_using_standard_regex_features(string itemTitle, string regex)
        {
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(title: itemTitle);

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void TitleMatching_should_throw_ReceiptItemAssertionFailedException_when_regex_matches_no_items(string regex)
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(regex);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void TitleMatching_should_throw_ReceiptItemAssertionFailedException_when_Title_of_all_items_is_null()
        {
            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(".*");

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void TitleMatching_should_throw_ReceiptItemAssertionFailedException_when_trying_to_capture_groups_but_Title_of_all_items_is_null()
        {
            IList<string> matches;

            var items = Enumerable.Range(1, 5).Select(_ => new ReceiptItem()).ToList();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
        }

        [Test]
        public void TitleMatching_should_not_output_matches_when_regex_does_not_match_Title_of_any_items()
        {
            IList<string> matches = null;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptItemAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Title_of_any_item()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            sut.TitleMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TitleMatching_should_output_matches_when_groupMatchingRegex_matches_Title_of_any_item()
        {
            IList<string> matches;

            const string someTitle = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(title: someTitle);

            var sut = new ReceiptItemSetAssertions(items);

            sut.TitleMatching(someTitle, $"({someTitle})", out matches);

            matches.First().Should().Be(someTitle);
        }

        [Test]
        public void TitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Title_several_times_for_a_single_item()
        {
            IList<string> matches;

            const string someTitle = "some text";
            var items = ReceiptItemTestData.CreateReceiptItemSetWithOneItemThatHasSetProperties(title: someTitle);

            var sut = new ReceiptItemSetAssertions(items);

            const string match1 = "some";
            const string match2 = "text";
            sut.TitleMatching(someTitle, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void TitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Title_on_multiple_items()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();
            items.Add(new ReceiptItem(title: "some text"));
            items.Add(new ReceiptItem(title: "same text"));

            var sut = new ReceiptItemSetAssertions(items);

            sut.TitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void TitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var items = ReceiptItemTestData.CreateRandomReceiptItems();

            var sut = new ReceiptItemSetAssertions(items);

            Action act = () => sut.TitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
