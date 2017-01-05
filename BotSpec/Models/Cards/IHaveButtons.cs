using System.Collections.Generic;

namespace BotSpec.Models.Cards
{
    internal interface IHaveButtons
    {
        /// <summary>
        /// Set of actions applicable to the current card
        /// </summary>
        IList<CardAction> Buttons { get; set; }
    }
}