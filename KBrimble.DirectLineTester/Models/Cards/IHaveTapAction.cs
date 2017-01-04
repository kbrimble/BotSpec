namespace KBrimble.DirectLineTester.Models.Cards
{
    internal interface IHaveTapAction
    {
        /// <summary>
        /// This action will be activated when user taps on the card itself
        /// </summary>
        CardAction Tap { get; set; }
    }
}