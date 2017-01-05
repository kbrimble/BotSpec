using System.Collections.Generic;

namespace BotSpec.Models.Cards
{
    internal interface IHaveImages
    {
        /// <summary>
        /// Array of images for the card
        /// </summary>
        IList<CardImage> Images { get; set; }
    }
}