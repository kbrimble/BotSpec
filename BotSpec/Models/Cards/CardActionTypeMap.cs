using System;

namespace BotSpec.Models.Cards
{
    internal class CardActionTypeMap
    {
        public static string Map(CardActionType type)
        {
            var typeName = Enum.GetName(typeof(CardActionType), type) ?? "Unknown";
            return char.ToLowerInvariant(typeName[0]) + typeName.Substring(1);
        }
    }
}