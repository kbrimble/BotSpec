using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public class MessageAssertions : IMessageAssertions
    {
        private readonly Message _message;

        public MessageAssertions(Message message)
        {
            _message = message;
        }

        public IMessageAssertions HaveTextMatching(string regex)
        {
            StringHelpers.TestForMatch(_message.Text, regex, new MessageAssertionFailedException());
            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_message.Text, regex, groupMatchRegex, new MessageAssertionFailedException());
            return this;
        }

        public IMessageAssertions BeFrom(string messageFrom)
        {
            if (messageFrom == null)
                throw new ArgumentNullException(nameof(messageFrom));

            if (!string.Equals(_message.FromProperty, messageFrom, StringComparison.InvariantCultureIgnoreCase))
                throw new MessageAssertionFailedException();

            return this;
        }

        public IMessageAttachmentAssertions HaveAttachment()
        {
            return new MessageAttachmentAssertions(_message);
        }
    }
}
