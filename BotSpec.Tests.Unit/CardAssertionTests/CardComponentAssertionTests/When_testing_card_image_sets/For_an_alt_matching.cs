using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_image_sets
{
    [TestFixture]
    public class For_an_alt_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void AltMatching_should_pass_if_regex_exactly_matches_message_Alt_of_one_card(string cardAltAndRegex)
        {
            var cardImages = CardImageTestData.CreateCardImageSetWithOneImageThatHasSetProperties(alt: cardAltAndRegex);

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(cardAltAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void AltMatching_should_pass_if_regex_exactly_matches_Alt_of_at_least_1_card_regardless_of_case(string alt, string regex)
        {
            var cardImages = CardImageTestData.CreateCardImageSetWithOneImageThatHasSetProperties(alt: alt);

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void AltMatching_should_pass_when_using_standard_regex_features(string alt, string regex)
        {
            var cardImages = CardImageTestData.CreateCardImageSetWithOneImageThatHasSetProperties(alt: alt);

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void AltMatching_should_throw_CardImageAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(regex);

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void AltMatching_should_throw_CardImageAssertionFailedException_when_Alt_of_all_cards_is_null()
        {
            var cardImages = Enumerable.Range(1, 5).Select(_ => new CardImage()).ToList();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(".*");

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void AltMatching_should_throw_CardImageAssertionFailedException_when_trying_to_capture_groups_but_Alt_of_all_cards_is_null()
        {
            IList<string> matches;

            var cardImages = Enumerable.Range(1, 5).Select(_ => new CardImage()).ToList();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(".*", "(.*)", out matches);

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void AltMatching_should_not_output_matches_when_regex_does_not_match_Alt_of_any_cards()
        {
            IList<string> matches = null;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardImageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void AltMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Alt_of_any_card()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            sut.AltMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void AltMatching_should_output_matches_when_groupMatchingRegex_matches_Alt_of_any_card()
        {
            IList<string> matches;

            const string someAlt = "some text";
            var cardImages = CardImageTestData.CreateCardImageSetWithOneImageThatHasSetProperties(alt: someAlt);

            var sut = new CardImageSetAssertions(cardImages);

            sut.AltMatching(someAlt, $"({someAlt})", out matches);

            matches.First().Should().Be(someAlt);
        }

        [Test]
        public void AltMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Alt_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someAlt = "some text";
            var cardImages = CardImageTestData.CreateCardImageSetWithOneImageThatHasSetProperties(alt: someAlt);

            var sut = new CardImageSetAssertions(cardImages);

            const string match1 = "some";
            const string match2 = "text";
            sut.AltMatching(someAlt, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void AltMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Alt_on_multiple_cards()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();
            cardImages.Add(new CardImage(alt: "some text"));
            cardImages.Add(new CardImage(alt: "same text"));

            var sut = new CardImageSetAssertions(cardImages);

            sut.AltMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void AltMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void AltMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void AltMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.AltMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
