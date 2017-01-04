using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    internal interface IHaveFacts
    {
        /// <summary>
        /// Array of Fact Objects   Array of key-value pairs.
        /// </summary>
        IList<Fact> Facts { get; set; }
    }
}