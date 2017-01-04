using System;
using Expecto.Exceptions;

namespace Expecto.Assertions
{
    public interface IThrow<out TEx> where TEx : BotAssertionFailedException
    {
        Func<TEx> CreateEx(string testedProperty, string regex);
    }
}
