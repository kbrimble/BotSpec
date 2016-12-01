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
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_message.Text, regex, CreateEx(nameof(_message.Text), regex));
            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_message.Text, regex, groupMatchRegex, CreateEx(nameof(_message.Text), regex));
            return this;
        }

        public IMessageAssertions HaveIdMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_message.Id, regex, CreateEx(nameof(_message.Id), regex));
            return this;
        }

        public IMessageAssertions HaveIdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_message.Id, regex, groupMatchRegex, CreateEx(nameof(_message.Id), regex));
            return this;
        }

        public IMessageAssertions HaveFromMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
                
            StringHelpers.TestForMatch(_message.FromProperty, regex, CreateEx(nameof(_message.FromProperty), regex));

            return this;
        }

        public IMessageAssertions HaveFromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_message.FromProperty, regex, groupMatchRegex, CreateEx(nameof(_message.FromProperty), regex));
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
