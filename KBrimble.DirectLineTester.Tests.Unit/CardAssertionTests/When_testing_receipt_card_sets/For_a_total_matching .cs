using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_receipt_card_sets
{
    [TestFixture]
    public class For_a_total_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasTotalMatching_should_pass_if_regex_exactly_matches_message_Total_of_one_card(string cardTotalAndRegex)
        {
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(total: cardTotalAndRegex);

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(cardTotalAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasTotalMatching_should_pass_if_regex_exactly_matches_Total_of_at_least_1_card_regardless_of_case(string cardTotal, string regex)
        {
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(total: cardTotal);

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasTotalMatching_should_pass_when_using_standard_regex_features(string cardTotal, string regex)
        {
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(total: cardTotal);

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasTotalMatching_should_throw_ReceiptCardAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cards = ReceiptCardTestData.CreateRandomReceiptCards();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(regex);

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
        }

        [Test]
        public void HasTotalMatching_should_throw_ReceiptCardAssertionFailedException_when_Total_of_all_cards_is_null()
        {
            var cards = Enumerable.Range(1, 5).Select(_ => new ReceiptCard()).ToList();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(".*");

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
        }

        [Test]
        public void HasTotalMatching_should_throw_ReceiptCardAssertionFailedException_when_trying_to_capture_groups_but_Total_of_all_cards_is_null()
        {
            IList<string> matches;

            var cards = Enumerable.Range(1, 5).Select(_ => new ReceiptCard()).ToList();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
        }

        [Test]
        public void HasTotalMatching_should_not_output_matches_when_regex_does_not_match_Total_of_any_cards()
        {
            IList<string> matches = null;

            var cards = ReceiptCardTestData.CreateRandomReceiptCards();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ReceiptCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasTotalMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Total_of_any_card()
        {
            IList<string> matches;

            var cards = ReceiptCardTestData.CreateRandomReceiptCards();

            var sut = new ReceiptCardSetAssertions(cards);

            sut.HasTotalMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasTotalMatching_should_output_matches_when_groupMatchingRegex_matches_Total_of_any_card()
        {
            IList<string> matches;

            const string someTotal = "some text";
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(total: someTotal);

            var sut = new ReceiptCardSetAssertions(cards);

            sut.HasTotalMatching(someTotal, $"({someTotal})", out matches);

            matches.First().Should().Be(someTotal);
        }

        [Test]
        public void HasTotalMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Total_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someTotal = "some text";
            var cards = ReceiptCardTestData.CreateReceiptCardSetWithOneCardThatHasSetProperties(total: someTotal);

            var sut = new ReceiptCardSetAssertions(cards);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasTotalMatching(someTotal, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasTotalMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Total_on_multiple_cards()
        {
            IList<string> matches;

            var cards = ReceiptCardTestData.CreateRandomReceiptCards();
            cards.Add(new ReceiptCard(total: "some text"));
            cards.Add(new ReceiptCard(total: "same text"));

            var sut = new ReceiptCardSetAssertions(cards);

            sut.HasTotalMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasTotalMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cards = ReceiptCardTestData.CreateRandomReceiptCards();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTotalMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var cards = ReceiptCardTestData.CreateRandomReceiptCards();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTotalMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cards = ReceiptCardTestData.CreateRandomReceiptCards();

            var sut = new ReceiptCardSetAssertions(cards);

            Action act = () => sut.HasTotalMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
