using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Expecto.Exceptions;

namespace Expecto.Assertions
{
    internal class StringHelpers<TEx> where TEx : BotAssertionFailedException
    {
        internal void TestForMatch(string input, string regex, Func<TEx> createException)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (createException == null)
                throw new ArgumentNullException(nameof(createException));

            if (input == null || !Regex.IsMatch(input.ToLowerInvariant(), regex, RegexOptions.IgnoreCase))
                throw createException();
        }

        internal IList<string> TestForMatchAndReturnGroups(string input, string regex, string groupMatchRegex, Func<TEx> createException)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (createException == null)
                throw new ArgumentNullException(nameof(createException));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestForMatch(input, regex, createException);

            IList<string> matchedGroups = null;

            var matches = Regex.Matches(input.ToLowerInvariant(), groupMatchRegex).Cast<Match>().ToList();
            if (matches.Any(m => m.Groups.Cast<Group>().Any()))
                matchedGroups = matches.SelectMany(m => m.Groups.Cast<Group>()).Select(g => g.Value).ToList();

            return matchedGroups;
        }
    }
}
