using System;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.MessageAssertionTests.When_testing_message_sets
{
    [TestFixture]
    public class For_from_matching
    {
        [Test]
        public void BeFrom_should_pass_if_at_least_1_message_exactly_matches_input()
        {
            var beFrom = "fromMe";
            var matchingMessage = new Message(fromProperty: beFrom);
            var messages = Enumerable.Range(1, 5).Select(i => new Message(fromProperty: $"from{i}")).ToList();
            messages.Add(matchingMessage);

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.BeFrom(beFrom);

            act.ShouldNotThrow<Exception>();
        }

        [Test]
        public void BeFrom_should_throw_MessageSetAssertionFailedException_if_no_messages_exactly_matches_input()
        {
            var messages = Enumerable.Range(1, 5).Select(i => new Message(fromProperty: $"from{i}")).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.BeFrom("not matching");

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_throw_MessageSetAssertionFailedException_if_FromProperty_of_all_message_is_null()
        {
            var messages = Enumerable.Range(1, 5).Select(i => new Message()).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.BeFrom("");

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_throw_ArgumentNullException_if_input_is_null()
        {
            var messages = Enumerable.Range(1, 5).Select(i => new Message(fromProperty: $"from{i}")).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.BeFrom(null);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
