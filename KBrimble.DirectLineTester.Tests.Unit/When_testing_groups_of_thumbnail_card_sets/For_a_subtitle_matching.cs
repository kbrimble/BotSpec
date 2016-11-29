using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.When_testing_groups_of_thumbnail_card_sets
{
    [TestFixture]
    public class For_a_subtitle_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasSubtitleMatching_should_pass_if_regex_exactly_matches_message_Subtitle_of_one_card_in_any_set(string cardSubtitleAndRegex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitleAndRegex).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(cardSubtitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasSubtitleMatching__should_pass_if_regex_exactly_matches_Subtitle_of_at_least_1_card_regardless_of_case_in_any_set(string cardSubtitle, string regex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasSubtitleMatching_should_pass_when_using_standard_regex_features(string cardSubtitle, string regex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void HasSubtitleMatching_should_throw_ThumbnailCardSetAssertionFailedException_when_regex_matches_no_cards_in_any_set(string cardSubtitle, string regex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(regex);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ThumbnailCardSetAssertionFailedException_when_Subtitle_of_all_cards_is_null_in_every_set()
        {
            var cards = ThumbnailCardSetTestData.CreateEmptyGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(".*");

            act.ShouldThrow<ThumbnailCardSetAssertionFailedException>();
        }

        //TODO: continue from here making htese groups
        [Test]
        public void HasSubtitleMatching_should_throw_ThumbnailCardSetAssertionFailedException_when_trying_to_capture_groups_but_Subtitle_of_all_cards_is_null()
        {
            IList<string> matches;

            var cards = Enumerable.Range(1, 5).Select(_ => new ThumbnailCard()).ToList();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ThumbnailCardSetAssertionFailedException>();
        }

        [Test]
        public void HasSubtitleMatching_should_not_output_matches_when_regex_does_not_match_Subtitle_of_any_cards()
        {
            IList<string> matches = null;

            var cards = ThumbnailCardSetTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ThumbnailCardSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasSubtitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Subtitle_of_any_card()
        {
            IList<string> matches = null;

            var thumbnailCard = new ThumbnailCard(subtitle: "some text");

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            Action act = () => sut.HasSubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasSubtitleMatching_should_output_matches_when_groupMatchingRegex_matches_Subtitle_of_any_card()
        {
            IList<string> matches;

            var thumbnailCard = new ThumbnailCard(subtitle: "some text");

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.HasSubtitleMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasSubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_on_multiple_cards()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateRandomThumbnailCards();
            cards.Add(new ThumbnailCard(subtitle: "some text"));
            cards.Add(new ThumbnailCard(subtitle: "same text"));

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.HasSubtitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cards = ThumbnailCardSetTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
