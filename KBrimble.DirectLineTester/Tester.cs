using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester
{
    public class Tester
    {
        readonly BotClient _client;
        readonly List<string> _matches;


        public Tester(string secretOrToken)
        {
            _client = new BotClient(secretOrToken);
            _matches = new List<string>();
        }

        public async Task ExpectMessageMatchingPattern(string messagePattern)
        {
            var messages = await _client.GetMessagesFromLowerWatermark().ConfigureAwait(false);
            foreach (var message in messages)
            {
                message.Should().HaveTextMatching(messagePattern);
            }
        }

        public async Task ExpectMessageMatchingPatternAndSaveMatches(string messagePattern, string matchPattern)
        {
            var messages = await _client.GetMessagesFromLowerWatermark().ConfigureAwait(false);
            foreach (var message in messages)
            {
                IEnumerable<string> matches;
                message.Should().HaveTextMatching(messagePattern, matchPattern, out matches);
                if (matches.Any())
                    _matches.AddRange(matches);
            }
        }
    }
}
