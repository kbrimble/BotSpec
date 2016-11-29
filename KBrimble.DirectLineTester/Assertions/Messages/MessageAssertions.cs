using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public class MessageAssertions : IMessageAssertions, IThrow<MessageAssertionFailedException>
    {
        private readonly Message _message;

        public MessageAssertions(Message message)
        {
            _message = message;
        }

        public IMessageAssertions HaveTextMatching(string regex)
        {
            StringHelpers.TestForMatch(_message.Text, regex, CreateEx(nameof(_message.Text), regex));
            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_message.Text, regex, groupMatchRegex, CreateEx(nameof(_message.Text), regex));
            return this;
        }

        public IMessageAssertions BeFrom(string messageFrom)
        {
            if (messageFrom == null)
                throw new ArgumentNullException(nameof(messageFrom));

            if (!string.Equals(_message.FromProperty, messageFrom, StringComparison.InvariantCultureIgnoreCase))
                throw CreateEx(nameof(_message.FromProperty), messageFrom);

            return this;
        }

        public IMessageAttachmentAssertions HaveAttachment()
        {
            return new MessageAttachmentAssertions(_message);
        }

        public MessageAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected message to have property {testedProperty} to match {regex} but regex test failed.";
            return new MessageAssertionFailedException(message);
        }
    }
}
