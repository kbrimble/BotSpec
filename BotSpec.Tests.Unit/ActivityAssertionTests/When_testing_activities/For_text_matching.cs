using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Activities;
using BotSpec.Exceptions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.ActivityAssertionTests.When_testing_activities
{
    [TestFixture]
    public class For_text_matching
    {
        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var activity = new Activity(text: "valid text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var activity = new Activity(text: "valid text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TextMatching_should_pass_if_regex_exactly_matches_activity_Text(string activityTextAndRegex)
        {
            var activity = new Activity(text: activityTextAndRegex);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(activityTextAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void TextMatching_should_pass_regardless_of_case(string activityText, string regex)
        {
            var activity = new Activity(text: activityText);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TextMatching_should_pass_when_using_standard_regex_features(string activityText, string regex)
        {
            var activity = new Activity(text: activityText);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void TextMatching_should_throw_ActivityAssertionFailedException_for_non_matching_regexes(string activityText, string regex)
        {
            var activity = new Activity(text: activityText);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(regex);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var activity = new Activity(text: "some text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var activity = new Activity(text: "some text");

            var sut = new ActivityAssertions(activity);

            sut.TextMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var activity = new Activity(text: someText);

            var sut = new ActivityAssertions(activity);

            sut.TextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void TextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var activity = new Activity(text: someText);

            var sut = new ActivityAssertions(activity);

            var match1 = "some";
            var match2 = "text";
            sut.TextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }


        [Test]
        public void TextMatching_should_throw_ActivityAssertionFailedException_when_text_is_null()
        {
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching("anything");

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_ActivityAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_trying_to_capture_groups_but_regex_is_null()
        {
            IList<string> matches;
            var activity = new Activity(text: "some text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.TextMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
