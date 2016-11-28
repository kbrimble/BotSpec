using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{
    internal static class StringHelpers
    {
        internal static void TestForMatch<T>(string input, string regex, T exceptionToThrow) where T : BotAssertionFailedException
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (exceptionToThrow == null)
                throw new ArgumentNullException(nameof(exceptionToThrow));

            if (input == null || !Regex.IsMatch(input.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw exceptionToThrow;
        }

        internal static IList<string> TestForMatchAndReturnGroups<T>(string input, string regex, string groupMatchRegex, T exceptionToThrow) where T : BotAssertionFailedException
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (exceptionToThrow == null)
                throw new ArgumentNullException(nameof(exceptionToThrow));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestForMatch(input, regex, exceptionToThrow);

            IList<string> matchedGroups = null;

            var matches = Regex.Matches(input.ToLowerInvariant(), groupMatchRegex).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

            return matchedGroups;
        }
    }

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
