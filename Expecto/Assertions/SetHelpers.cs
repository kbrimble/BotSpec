using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Exceptions;

namespace Expecto.Assertions
{
    internal class SetHelpers<TSetItem, TEx> where TEx : BotAssertionFailedException
    {
        internal delegate void TestWithGroups(TSetItem item, out IList<string> groupMatches);

        internal void TestSetForMatch(IEnumerable<TSetItem> set, Action<TSetItem> test, Func<TEx> exceptionToThrow)
        {
            var passedAssertion = false;
            foreach (var item in set)
            {
                try
                {
                    test(item);
                }
                catch (TEx)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw exceptionToThrow();
        }

        internal IList<string> TestSetForMatchAndReturnGroups(IEnumerable<TSetItem> set, TestWithGroups testWithGroups, Func<TEx> exceptionToThrow)
        {
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var item in set)
            {
                try
                {
                    IList<string> matches;
                    testWithGroups(item, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (TEx)
                {
                    continue;
                }
                passedAssertion = true;
            }

            if (!passedAssertion)
                throw exceptionToThrow();

            return totalMatches.Any() ? totalMatches : null;
        }
    }
}