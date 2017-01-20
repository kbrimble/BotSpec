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
    public class For_from_matching
    {
        [Test]
        public void FromMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var activity = new Activity(fromProperty: new ChannelAccount(name: "valid text"));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void FromMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var activity = new Activity(fromProperty: new ChannelAccount(name: "valid text"));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void FromMatching_should_pass_if_regex_exactly_matches_activity_From(string activityFromAndRegex)
        {
            var activity = new Activity(fromProperty: new ChannelAccount(name: activityFromAndRegex));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(activityFromAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void FromMatching_should_pass_regardless_of_case(string activityFrom, string regex)
        {
            var activity = new Activity(fromProperty: new ChannelAccount(name: activityFrom));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void FromMatching_should_pass_when_using_standard_regex_features(string activityFrom, string regex)
        {
            var activity = new Activity(fromProperty: new ChannelAccount(name: activityFrom));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void FromMatching_should_throw_ActivityAssertionFailedException_for_non_matching_regexes(string activityFrom, string regex)
        {
            var activity = new Activity(fromProperty: new ChannelAccount(name: activityFrom));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(regex);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void FromMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var activity = new Activity(fromProperty: new ChannelAccount(name: "some text"));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void FromMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var activity = new Activity(fromProperty: new ChannelAccount(name: "some text"));

            var sut = new ActivityAssertions(activity);

            sut.FromMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void FromMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someFrom = "some text";
            var activity = new Activity(fromProperty: new ChannelAccount(name: someFrom));

            var sut = new ActivityAssertions(activity);

            sut.FromMatching(someFrom, $"({someFrom})", out matches);

            matches.First().Should().Be(someFrom);
        }

        [Test]
        public void FromMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someFrom = "some text";
            var activity = new Activity(fromProperty: new ChannelAccount(name: someFrom));

            var sut = new ActivityAssertions(activity);

            var match1 = "some";
            var match2 = "text";
            sut.FromMatching(someFrom, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }


        [Test]
        public void FromMatching_should_throw_ActivityAssertionFailedException_when_text_is_null()
        {
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching("anything");

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void FromMatching_should_throw_ActivityAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void FromMatching_should_throw_ArgumentNullException_when_trying_to_capture_groups_but_regex_is_null()
        {
            IList<string> matches;
            var activity = new Activity(fromProperty: new ChannelAccount(name: "some text"));

            var sut = new ActivityAssertions(activity);

            Action act = () => sut.FromMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

    }
}
