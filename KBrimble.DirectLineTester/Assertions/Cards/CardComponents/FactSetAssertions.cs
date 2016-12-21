using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public class FactSetAssertions : IFactAssertions
    {
        public readonly IEnumerable<Fact> Facts;
        private readonly SetHelpers<Fact, FactAssertionFailedException> _setHelper;

        public FactSetAssertions(IEnumerable<Fact> facts) : this()
        {
            if (facts == null)
                throw new ArgumentNullException(nameof(facts));
            Facts = facts.Where(fact => fact != null);
        }

        public FactSetAssertions(IHaveFacts iHaveFacts)
        {
            if (iHaveFacts?.Facts == null)
                throw new ArgumentNullException(nameof(iHaveFacts.Facts));

            Facts = iHaveFacts.Facts.Where(fact => fact != null);
        }

        private FactSetAssertions()
        {
            _setHelper = new SetHelpers<Fact, FactAssertionFailedException>();
        }

        public IFactAssertions HasKeyMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelper.TestSetForMatch(Facts, fact => fact.That().HasKeyMatching(regex), CreateEx(nameof(Fact.Key), regex));

            return this;
        }

        public IFactAssertions HasKeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Fact, FactAssertionFailedException>.TestWithGroups act =
                (Fact fact, out IList<string> matches) => fact.That().HasKeyMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelper.TestSetForMatchAndReturnGroups(Facts, act, CreateEx(nameof(Fact.Key), regex));

            return this;
        }

        public IFactAssertions HasValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelper.TestSetForMatch(Facts, fact => fact.That().HasValueMatching(regex), CreateEx(nameof(Fact.Key), regex));

            return this;
        }

        public IFactAssertions HasValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Fact, FactAssertionFailedException>.TestWithGroups act =
                (Fact fact, out IList<string> matches) => fact.That().HasValueMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelper.TestSetForMatchAndReturnGroups(Facts, act, CreateEx(nameof(Fact.Key), regex));

            return this;

        }

        public Func<FactAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected property {testedProperty} to match {regex} but did not";
            return () => new FactAssertionFailedException(message);
        }
    }
}