using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.When_testing_thumbnail_card_sets
{
    [TestFixture]
    public class For_a_title_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasTitleMatching_should_pass_if_regex_exactly_matches_message_Text_of_one_card(string cardTextAndRegex)
        {
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneMessageThatHasSetProperties(title: cardTextAndRegex);

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(cardTextAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasTitleMatching_should_pass_if_regex_exactly_matches_text_of_at_least_1_card_regardless_of_case(string cardText, string regex)
        {
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneMessageThatHasSetProperties(title: cardText);

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasTitleMatching_should_pass_when_using_standard_regex_features(string cardText, string regex)
        {
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneMessageThatHasSetProperties(title: cardText);

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasTitleMatching_should_throw_ThumbnailCardSetAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldThrow<ThumbnailCardSetAssertionFailedException>();
        }

        [Test]
        public void HasTitleMatching_should_not_output_matches_when_regex_does_not_match_text_of_any_cards()
        {
            IList<string> matches = null;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ThumbnailCardSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasTitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text_of_any_card()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.HasTitleMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasTitleMatching_should_output_matches_when_groupMatchingRegex_matches_text_of_any_card()
        {
            IList<string> matches;

            const string someText = "some text";
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneMessageThatHasSetProperties(title: someText);

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.HasTitleMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void HasTitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someText = "some text";
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneMessageThatHasSetProperties(title: someText);

            var sut = new ThumbnailCardSetAssertions(cards);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasTitleMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasTitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_on_multiple_cards()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();
            cards.Add(new ThumbnailCard(title: "some text"));
            cards.Add(new ThumbnailCard(title: "same text"));

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.HasTitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasTitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
