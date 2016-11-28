using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Assertions;
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
            matchedGroups = null;

            HaveTextMatching(regex);

            var matches = Regex.Matches(_message.Text.ToLowerInvariant(), groupMatchRegex).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

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
