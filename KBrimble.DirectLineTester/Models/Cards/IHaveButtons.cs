using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    internal interface IHaveButtons
    {
        /// <summary>
        /// Set of actions applicable to the current card
        /// </summary>
        IList<CardAction> Buttons { get; set; }
    }
}