using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    public interface IHaveFacts
    {
        /// <summary>
        /// Array of Fact Objects   Array of key-value pairs.
        /// </summary>
        IList<Fact> Facts { get; set; }
    }
}