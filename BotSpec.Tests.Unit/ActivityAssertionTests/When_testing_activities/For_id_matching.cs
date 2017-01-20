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
    public class For_id_matching
    {
        [Test]
        public void IdMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var activity = new Activity(id: "valid text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void IdMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var activity = new Activity(id: "valid text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void IdMatching_should_pass_if_regex_exactly_matches_activity_Id(string activityIdAndRegex)
        {
            var activity = new Activity(id: activityIdAndRegex);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(activityIdAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void IdMatching_should_pass_regardless_of_case(string activityId, string regex)
        {
            var activity = new Activity(id: activityId);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void IdMatching_should_pass_when_using_standard_regex_features(string activityId, string regex)
        {
            var activity = new Activity(id: activityId);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void IdMatching_should_throw_ActivityAssertionFailedException_for_non_matching_regexes(string activityId, string regex)
        {
            var activity = new Activity(id: activityId);

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(regex);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void IdMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var activity = new Activity(id: "some text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void IdMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var activity = new Activity(id: "some text");

            var sut = new ActivityAssertions(activity);

            sut.IdMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void IdMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someId = "some text";
            var activity = new Activity(id: someId);

            var sut = new ActivityAssertions(activity);

            sut.IdMatching(someId, $"({someId})", out matches);

            matches.First().Should().Be(someId);
        }

        [Test]
        public void IdMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someId = "some text";
            var activity = new Activity(id: someId);

            var sut = new ActivityAssertions(activity);

            var match1 = "some";
            var match2 = "text";
            sut.IdMatching(someId, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }


        [Test]
        public void IdMatching_should_throw_ActivityAssertionFailedException_when_text_is_null()
        {
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching("anything");

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void IdMatching_should_throw_ActivityAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void IdMatching_should_throw_ArgumentNullException_when_trying_to_capture_groups_but_regex_is_null()
        {
            IList<string> matches;
            var activity = new Activity(id: "some text");

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.IdMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
