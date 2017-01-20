using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Activities;
using BotSpec.Exceptions;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.ActivityAssertionTests.When_testing_activity_sets
{
    [TestFixture]
    public class For_id_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void IdMatching_should_pass_if_regex_exactly_matches_activity_Id_of_one_activity(string idAndRegex)
        {
            var activities = ActivityTestData.CreateActivitySetWithOneActivityThatHasSetProperties(id: idAndRegex);

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(idAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void IdMatching_should_pass_if_regex_exactly_matches_Id_of_at_least_1_activity_regardless_of_case(string text, string regex)
        {
            var activities = ActivityTestData.CreateActivitySetWithOneActivityThatHasSetProperties(id: text);

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void IdMatching_should_pass_when_using_standard_regex_features(string text, string regex)
        {
            var activities = ActivityTestData.CreateActivitySetWithOneActivityThatHasSetProperties(id: text);

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void IdMatching_should_throw_ActivityAssertionFailedException_when_regex_matches_no_activities(string regex)
        {
            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(regex);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void IdMatching_should_throw_ActivityAssertionFailedException_when_Id_of_all_activities_is_null()
        {
            var activities = Enumerable.Range(1, 5).Select(_ => new Activity()).ToList();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(".*");

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void IdMatching_should_throw_ActivityAssertionFailedException_when_trying_to_capture_groups_but_Id_of_all_activities_is_null()
        {
            IList<string> matches;

            var activities = Enumerable.Range(1, 5).Select(_ => new Activity()).ToList();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void IdMatching_should_not_output_matches_when_regex_does_not_match_Id_of_any_activities()
        {
            IList<string> matches = null;

            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ActivityAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void IdMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Id_of_any_activity()
        {
            IList<string> matches;

            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            sut.IdMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void IdMatching_should_output_matches_when_groupMatchingRegex_matches_Id_of_any_activity()
        {
            IList<string> matches;

            const string someId = "some text";
            var activities = ActivityTestData.CreateActivitySetWithOneActivityThatHasSetProperties(id: someId);

            var sut = new ActivitySetAssertions(activities);

            sut.IdMatching(someId, $"({someId})", out matches);

            matches.First().Should().Be(someId);
        }

        [Test]
        public void IdMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Id_several_times_for_a_single_activity()
        {
            IList<string> matches;

            const string someId = "some text";
            var activities = ActivityTestData.CreateActivitySetWithOneActivityThatHasSetProperties(id: someId);

            var sut = new ActivitySetAssertions(activities);

            const string match1 = "some";
            const string match2 = "text";
            sut.IdMatching(someId, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void IdMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Id_on_multiple_activities()
        {
            IList<string> matches;

            var activities = ActivityTestData.CreateRandomActivities();
            activities.Add(new Activity(id: "some text"));
            activities.Add(new Activity(id: "same text"));

            var sut = new ActivitySetAssertions(activities);

            sut.IdMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void IdMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void IdMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void IdMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            Action act = () => sut.IdMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

    }
}
