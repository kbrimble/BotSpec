using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{
    internal class SetHelpers<TSetItem, TExCatch, TExThrow> 
        where TExCatch : BotAssertionFailedException 
        where TExThrow : BotAssertionFailedException
    {
        internal delegate void TestWithGroups(TSetItem item, out IList<string> groupMatches);

        internal void TestSetForMatch(IEnumerable<TSetItem> set, Action<TSetItem> test, TExThrow exceptionToThrow)
        {
            var passedAssertion = false;
            foreach (var item in set)
            {
                try
                {
                    test(item);
                }
                catch (TExCatch)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw exceptionToThrow;
        }

        internal IList<string> TestSetForMatchAndReturnGroups(IEnumerable<TSetItem> set, TestWithGroups testWithGroups, TExThrow exceptionToThrow)
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
                catch (TExCatch)
                {
                    continue;
                }
                passedAssertion = true;
            }

            if (!passedAssertion)
                throw exceptionToThrow;

            return totalMatches.Any() ? totalMatches : null;
        }
    }
}