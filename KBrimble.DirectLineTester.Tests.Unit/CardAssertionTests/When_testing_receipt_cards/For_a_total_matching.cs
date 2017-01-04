using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_receipt_cards
{
    [TestFixture]
    public class For_a_total_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TotalMatching_should_pass_if_regex_exactly_matches_message_Total(string cardTotalAndRegex)
        {
            var receiptCard = new ReceiptCard(total: cardTotalAndRegex);

            var sut = new ReceiptCardAssertions(receiptCard);

            Action act = () => sut.TotalMatching(cardTotalAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void TotalMatching_should_pass_regardless_of_case(string cardTotal, string regex)
        {
            var receiptCard = new ReceiptCard(total: cardTotal);

            var sut = new ReceiptCardAssertions(receiptCard);

            Action act = () => sut.TotalMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TotalMatching_should_pass_when_using_standard_regex_features(string cardTotal, string regex)
        {
            var receiptCard = new ReceiptCard(total: cardTotal);

            var sut = new ReceiptCardAssertions(receiptCard);

            Action act = () => sut.TotalMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void TotalMatching_should_throw_ReceiptCardAssertionFailedException_for_non_matching_regexes(string cardTotal, string regex)
        {
            var receiptCard = new ReceiptCard(total: cardTotal);

            var sut = new ReceiptCardAssertions(receiptCard);

            Action act = () => sut.TotalMatching(regex);

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
        }

        [Test]
        public void TotalMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var receiptCard = new ReceiptCard(total: "some text");

            var sut = new ReceiptCardAssertions(receiptCard);

            Action act = () => sut.TotalMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TotalMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var receiptCard = new ReceiptCard(total: "some text");

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.TotalMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TotalMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var receiptCard = new ReceiptCard(total: someText);

            var sut = new ReceiptCardAssertions(receiptCard);

            sut.TotalMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void TotalMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var receiptCard = new ReceiptCard(total: someText);

            var sut = new ReceiptCardAssertions(receiptCard);

            const string match1 = "some";
            const string match2 = "text";
            sut.TotalMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void TotalMatching_should_throw_ReceiptCardAssertionFailedException_when_text_is_null()
        {
            var card = new ReceiptCard();

            var sut = new ReceiptCardAssertions(card);

            Action act = () => sut.TotalMatching("anything");

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
        }

        [Test]
        public void TotalMatching_should_throw_ReceiptCardAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var card = new ReceiptCard();

            var sut = new ReceiptCardAssertions(card);

            Action act = () => sut.TotalMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
        }

        [Test]
        public void TotalMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new ReceiptCard();

            var sut = new ReceiptCardAssertions(card);

            Action act = () => sut.TotalMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TotalMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new ReceiptCard();

            var sut = new ReceiptCardAssertions(card);

            Action act = () => sut.TotalMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TotalMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new ReceiptCard();

            var sut = new ReceiptCardAssertions(card);

            Action act = () => sut.TotalMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
