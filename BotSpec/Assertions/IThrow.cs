using System;
using BotSpec.Exceptions;

namespace BotSpec.Assertions
{
    public interface IThrow<out TEx> where TEx : BotAssertionFailedException
    {
        Func<TEx> CreateEx(string testedProperty, string regex);
    }
}
