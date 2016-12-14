using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IFactAssertions
    {
        IFactAssertions HasKeyMatching(string regex);
        IFactAssertions HasKeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IFactAssertions HasValueMatching(string regex);
        IFactAssertions HasValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }

    public class FactAssertions : IFactAssertions, IThrow<FactAssertionFailedException>
    {
        private readonly Fact _fact;
        private readonly StringHelpers<FactAssertionFailedException> _stringHelpers;

        public FactAssertions(Fact fact)
        {
            _fact = fact;
            _stringHelpers = new StringHelpers<FactAssertionFailedException>();
        }

        public IFactAssertions HasKeyMatching(string regex)
        {
            _stringHelpers.TestForMatch(_fact.Key, regex, CreateEx(nameof(_fact.Key), regex));

            return this;
        }

        public IFactAssertions HasKeyMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_fact.Key, regex, groupMatchRegex, CreateEx(nameof(_fact.Key), regex));

            return this;
        }

        public IFactAssertions HasValueMatching(string regex)
        {
            throw new NotImplementedException();
        }

        public IFactAssertions HasValueMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            throw new NotImplementedException();
        }

        public Func<FactAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected fact with property {testedProperty} that matches {regex} but regex test failed";
            return () => new FactAssertionFailedException(message);
        }
    }

    public interface ICanAssertFacts
    {
        IFactAssertions WithFactThat();
    }

    public interface IReceiptItemAssertions : ICanAssertTapActions, ICanAssertCardImages, ICanAssertFacts
    {
        
    }

    public interface ICanAssertReceiptItems
    {
        IReceiptItemAssertions WithReceiptItemThat();
    }

    public interface IReceiptCardAssertions : ICanAssertButtons, ICanAssertTapActions, ICanAssertCardImages, ICanAssertReceiptItems
    {
        
    }

    
}
