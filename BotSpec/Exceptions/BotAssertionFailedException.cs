﻿using System;

namespace BotSpec.Exceptions
{
    public abstract class BotAssertionFailedException : Exception
    {
        protected BotAssertionFailedException(string message) : base(message) { }
    }

    public class FactAssertionFailedException : BotAssertionFailedException
    {
        public FactAssertionFailedException(string message) : base(message) {}
    }
}
