using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{
    internal static class SetHelpers
    {
        internal delegate void TestWithGroups<in TSetItem>(TSetItem item, out IList<string> groupMatches);

        internal static void TestSetForMatch<TSetItem, TEx>(IEnumerable<TSetItem> set, Action<TSetItem> test, Type exceptionToCatch, TEx exceptionToThrow) where TEx : BotAssertionFailedException
        {
            var passedAssertion = false;
            foreach (var item in set)
            {
                try
                {
                    test(item);
                }
                catch (Exception e)
                {
                    if (e.GetType() == exceptionToCatch)
                        continue;
                    throw;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw exceptionToThrow;
        }

        internal static IList<string> TestSetForMatchAndReturnGroups<TSetItem, TEx>(IEnumerable<TSetItem> set, TestWithGroups<TSetItem> testWithGroups, Type exceptionToCatch, TEx exceptionToThrow) where TEx : BotAssertionFailedException
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
                catch (Exception e)
                {
                    if (e.GetType() == exceptionToCatch)
                        continue;
                    throw;
                }
                passedAssertion = true;
            }

            if (!passedAssertion)
                throw exceptionToThrow;

            return totalMatches.Any() ? totalMatches : null;
        }
    }
}