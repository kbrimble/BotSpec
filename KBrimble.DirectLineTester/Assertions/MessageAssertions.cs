using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions
{
    public class MessageAssertions : IMessageAssertions
    {
        readonly Message message;

        public MessageAssertions(Message message)
        {
            this.message = message;
        }

        public IMessageAssertions HaveTextMatching(string regex)
        {
            var text = message.Text.ToLowerInvariant();
            if (!Regex.IsMatch(text, regex, RegexOptions.IgnoreCase))
                throw new MessageAssertionFailedException();
            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;

            if (!Regex.IsMatch(message.Text.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw new MessageAssertionFailedException();

            var matches = Regex.Matches(message.Text.ToLowerInvariant(), groupMatchRegex).Cast<Match>();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value);

            return this;
        }

        public IMessageAssertions BeFrom(string messageFrom)
        {
            if (message.FromProperty.ToLowerInvariant() != messageFrom.ToLowerInvariant())
                throw new MessageAssertionFailedException();

            return this;
        }

        public IMessageAttachmentAssertions WithAttachment()
        {
            return new MessageAttachementAssertions(message);
        }
    }
}
