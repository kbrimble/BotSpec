using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public interface IFactAssertions
    {
        IFactAssertions HasKeyMatching(string regex);
        IFactAssertions HasKeyMatching(string regex, string groupMatchRegex, out IList<string> groupMatches);
        IFactAssertions HasValueMatching(string regex);
        IFactAssertions HasValueMatching(string regex, string groupMatchRegex, out IList<string> groupMatches);
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
