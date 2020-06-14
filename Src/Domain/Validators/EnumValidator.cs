using System;
using System.Collections;
using System.Linq;

namespace FactorioProductionCells.Domain.Validators
{
    public static class EnumValidator
    {
        public static void ValidateIntIdInEnumRange<T>(int value, String propertyName) where T : System.Enum
        {
            if(!Enum.IsDefined(typeof(T), value)) throw new ArgumentOutOfRangeException($"The value specified for {propertyName} is outside the valid range of identifiers.", propertyName);
        }
    }
}
