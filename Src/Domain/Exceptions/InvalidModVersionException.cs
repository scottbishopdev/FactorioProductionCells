using System;

namespace FactorioProductionCells.Domain.Exceptions
{
    public class InvalidModVersionException : Exception
    {
        public InvalidModVersionException() {}

        public InvalidModVersionException(string message) : base(message) {}

        public InvalidModVersionException(string message, Exception inner) : base(message, inner) {}
    }
}
