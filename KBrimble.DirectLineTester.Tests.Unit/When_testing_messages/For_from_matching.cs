using System;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.When_testing_messages
{
    [TestFixture]
    public class For_from_matching
    {
        [Test]
        public void BeFrom_should_throw_MessageAssertionFailedException_if_input_does_not_match_FromProperty_of_message()
        {
            var fromValue = "fromMe";
            var message = new Message(fromProperty: fromValue);

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom("someoneElse");

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_throw_MessageAssertionFailedException_if_FromProperty_of_message_is_null()
        {
            var message = new Message();

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom("");

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_not_throw_if_input_only_differers_message_FromProperty_in_case()
        {
            var fromValue = "FROMME";
            var message = new Message(fromProperty: fromValue);

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom("fromMe");

            act.ShouldNotThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_not_throw_if_input_exactly_matches_message_FromProperty()
        {
            var fromValue = "fromMe";
            var message = new Message(fromProperty: fromValue);

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom(fromValue);

            act.ShouldNotThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_throw_ArgumentNullException_if_input_is_null()
        {
            var message = new Message(fromProperty: "fromMe");

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom(null);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
