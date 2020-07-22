using System;

namespace FactorioProductionCells.Domain.Validators
{
    public static class ObjectValidator
    {
        public static void ValidateRequiredObject(Object value, String propertyName)
        {
            if(value == null) throw new ArgumentNullException(propertyName, $"{propertyName} is required.");
        }
    }
}
