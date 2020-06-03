using System;

namespace FactorioProductionCells.Domain.Exceptions
{
    public class InvalidFactorioVersionException : Exception
    {
        public InvalidFactorioVersionException() {}

        public InvalidFactorioVersionException(string message) : base(message) {}

        public InvalidFactorioVersionException(string message, Exception inner) : base(message, inner) {}
    }
}
