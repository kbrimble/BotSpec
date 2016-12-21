using System;
using KBrimble.DirectLineTester.Exceptions;

namespace KBrimble.DirectLineTester.Assertions
{
    public interface IThrow<out TEx> where TEx : BotAssertionFailedException
    {
        Func<TEx> CreateEx(string testedProperty, string regex);
    }
}
