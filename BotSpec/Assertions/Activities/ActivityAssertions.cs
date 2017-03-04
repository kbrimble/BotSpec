using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Attachments;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Activities
{
    internal class ActivityAssertions : IActivityAssertions, IThrow<ActivityAssertionFailedException>
    {
        private readonly Activity _activity;
        private readonly StringHelpers<ActivityAssertionFailedException> _stringHelpers;

        public ActivityAssertions(Activity activity)
        {
            _activity = activity;
            _stringHelpers = new StringHelpers<ActivityAssertionFailedException>();
        }

        public IActivityAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_activity.Text, regex, CreateEx(nameof(_activity.Text), regex));
            return this;
        }

        public IActivityAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_activity.Text, regex, groupMatchRegex, CreateEx(nameof(_activity.Text), regex));
            return this;
        }

        public IActivityAssertions IdMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_activity.Id, regex, CreateEx(nameof(_activity.Id), regex));
            return this;
        }

        public IActivityAssertions IdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_activity.Id, regex, groupMatchRegex, CreateEx(nameof(_activity.Id), regex));
            return this;
        }

        public IActivityAssertions FromMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
                
            _stringHelpers.TestForMatch(_activity?.From?.Name, regex, CreateEx(nameof(_activity.From), regex));

            return this;
        }

        public IActivityAssertions FromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_activity?.From?.Name, regex, groupMatchRegex, CreateEx(nameof(_activity.From), regex));
            return this;
        }

        public IActivityAssertions TextMatchingStrings(string[] strings)
        {
            if (strings == null)
                throw new ArgumentNullException(nameof(strings));
            throw new NotImplementedException();
        }

        public IActivityAttachmentAssertions WithAttachment()
        {
            return new ActivityAttachmentAssertions(_activity);
        }

        public Func<ActivityAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected activity to have property {testedProperty} to match {regex} but regex test failed.";
            return () => new ActivityAssertionFailedException(message);
        }
    }
}
