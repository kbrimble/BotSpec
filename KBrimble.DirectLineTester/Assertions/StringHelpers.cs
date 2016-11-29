using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{
    internal static class StringHelpers
    {
        internal static void TestForMatch<TEx>(string input, string regex, TEx exceptionToThrow) where TEx : BotAssertionFailedException
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (exceptionToThrow == null)
                throw new ArgumentNullException(nameof(exceptionToThrow));

            if (input == null || !Regex.IsMatch(input.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw exceptionToThrow;
        }

        internal static IList<string> TestForMatchAndReturnGroups<TEx>(string input, string regex, string groupMatchRegex, TEx exceptionToThrow) where TEx : BotAssertionFailedException
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
}
