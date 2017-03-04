using System;
using System.Linq;
using BotSpec.Assertions.Activities;
using BotSpec.Exceptions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.ActivityAssertionTests.When_testing_activities
{ 
    [TestFixture]
    public class For_text_matching_strings
    {
        [Test]
        public void TextMatchingStrings_should_throw_ArgumentNullException_if_string_array_is_null()
        {
            var activity = new Activity(text: "some text");
            var sut = new ActivityAssertions(activity);
            Action act = () => sut.TextMatchingStrings(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatchingStrings_should_throw_ActivityAssertionFailedException_if_string_array_is_empty()
        {
            var activity = new Activity(text: "some text");
            var sut = new ActivityAssertions(activity);
            Action act = () => sut.TextMatchingStrings(new string[1]);
            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void TextMatchingStrings_should_throw_ActivityAssertionFailedException_if_no_strings_match_activity_text()
        {
            var activity = new Activity(text: "some text");
            var sut = new ActivityAssertions(activity);
            var randomStrings = Enumerable.Range(1, 10).Select(_ => RandomString(10)).ToArray();
            Action act = () => sut.TextMatchingStrings(randomStrings);
            act.ShouldThrow<ActivityAssertionFailedException>();
        }

        [Test]
        public void TextMatchingStrings_should_pass_if_one_string_exactly_matches_activity_text()
        {
            const string activityText = "some text";
            var activity = new Activity(text: activityText);
            var sut = new ActivityAssertions(activity);
            var strings = Enumerable.Range(1, 10).Select(_ => RandomString(10)).ToList();
            strings.Add(activityText);
            Action act = () => sut.TextMatchingStrings(strings.ToArray());
            act.ShouldNotThrow<Exception>();
        }

        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
