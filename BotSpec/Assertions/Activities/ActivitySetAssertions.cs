using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Attachments;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;
using BotSpec.Assertions.Cards.CardComponents;

namespace BotSpec.Assertions.Activities
{
    internal class ActivitySetAssertions : IActivityAssertions, IThrow<ActivityAssertionFailedException>
    {
        private readonly IEnumerable<Activity> _messageSet;
        private readonly SetHelpers<Activity, ActivityAssertionFailedException> _setHelpers;

        public ActivitySetAssertions(IList<Activity> messageSet)
        {
            if (messageSet == null || !messageSet.Any())
                throw new ActivityAssertionFailedException("No activity received from bot");

            _messageSet = messageSet;
            _setHelpers = new SetHelpers<Activity, ActivityAssertionFailedException>();
        }

        public IActivityAssertions FromMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().FromMatching(regex), CreateEx(nameof(Activity.From), regex));

            return this;
        }

        public IActivityAssertions FromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Activity, ActivityAssertionFailedException>.TestWithGroups act
                = (Activity item, out IList<string> matches) => item.Should().FromMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Activity.From), regex));

            return this;

        }

        public IActivityAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().TextMatching(regex), CreateEx(nameof(Activity.Text), regex));

            return this;
        }

        public IActivityAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Activity, ActivityAssertionFailedException>.TestWithGroups act
                = (Activity item, out IList<string> matches) => item.Should().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Activity.Text), regex));

            return this;
        }

        public IActivityAssertions IdMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().IdMatching(regex), CreateEx(nameof(Activity.Id), regex));

            return this;
        }

        public IActivityAssertions IdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Activity, ActivityAssertionFailedException>.TestWithGroups act
                = (Activity item, out IList<string> matches) => item.Should().IdMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Activity.Id), regex));

            return this;
        }

        public IActivityAttachmentAssertions WithAttachment()
        {
            return new ActivitySetAttachmentAssertions(_messageSet);
        }

        public ICardActionAssertions WithSuggestedActions()
        {
            var actions = _messageSet.Where(message => message.SuggestedActions?.Actions != null).SelectMany(message => message.SuggestedActions.Actions).ToList();
            return new CardActionSetAssertions(actions);
        }

        public Func<ActivityAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected one message in set to have property {testedProperty} to match {regex} but none did.";
            return () => new ActivityAssertionFailedException(message);
        }
    }
}
