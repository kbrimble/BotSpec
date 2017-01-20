using System;
using System.Collections.Generic;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards.CardComponents
{
    internal class FactAssertions : IFactAssertions, IThrow<FactAssertionFailedException>
    {
        private readonly Fact _fact;
        private readonly StringHelpers<FactAssertionFailedException> _stringHelpers;

        public FactAssertions(Fact fact) : this()
        {
            if (fact == null)
                throw new ArgumentNullException(nameof(fact));

            _fact = fact;
        }

        private FactAssertions()
        {
            _stringHelpers = new StringHelpers<FactAssertionFailedException>();
        }

        public IFactAssertions KeyMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_fact.Key, regex, CreateEx(nameof(_fact.Key), regex));

            return this;
        }

        public IFactAssertions KeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_fact.Key, regex, groupMatchRegex, CreateEx(nameof(_fact.Key), regex));

            return this;
        }

        public IFactAssertions ValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_fact.Value, regex, CreateEx(nameof(_fact.Value), regex));

            return this;
        }

        public IFactAssertions ValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_fact.Value, regex, groupMatchRegex, CreateEx(nameof(_fact.Value), regex));

            return this;
        }

        public Func<FactAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected fact with property {testedProperty} that matches {regex} but regex test failed";
            return () => new FactAssertionFailedException(message);
        }
    }
}