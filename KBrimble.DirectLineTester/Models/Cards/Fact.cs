namespace KBrimble.DirectLineTester.Models.Cards
{
    internal class Fact
    {
        /// <summary>
        /// Initializes a new instance of the Fact class.
        /// </summary>
        public Fact() { }

        /// <summary>
        /// Initializes a new instance of the Fact class.
        /// </summary>
        public Fact(string key = default(string), string value = default(string))
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// The key for this Fact
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value for this Fact
        /// </summary>
        public string Value { get; set; }

    }
}