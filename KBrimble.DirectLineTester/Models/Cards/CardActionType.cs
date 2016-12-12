namespace KBrimble.DirectLineTester.Models.Cards
{
    public class CardActionType
    {
        private CardActionType(string type)
        {
            Value = type;
        }

        public string Value { get; }

        public static readonly CardActionType OpenUrl = new CardActionType("openUrl");
        public static readonly CardActionType ImBack = new CardActionType("imBack");
        public static readonly CardActionType PostBack = new CardActionType("postBack");
        public static readonly CardActionType Call = new CardActionType("call");
        public static readonly CardActionType PlayAudio = new CardActionType("playAudio");
        public static readonly CardActionType PlayVideo = new CardActionType("playVideo");
        public static readonly CardActionType ShowImage = new CardActionType("showImage");
        public static readonly CardActionType DownloadFile = new CardActionType("downloadFile");
        public static readonly CardActionType Signin = new CardActionType("signin");

        public static readonly CardActionType[] AllTypes
            = { OpenUrl, ImBack, PostBack, Call, PlayAudio, PlayVideo, ShowImage, DownloadFile, Signin };
    }
}