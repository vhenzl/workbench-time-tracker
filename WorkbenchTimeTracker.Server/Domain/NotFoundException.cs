﻿namespace WorkbenchTimeTracker.Server.Domain
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
