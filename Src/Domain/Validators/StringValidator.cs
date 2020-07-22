using System;

namespace FactorioProductionCells.Domain.Validators
{
    public static class StringValidator
    {
        public static void ValidateRequiredString(String value, String propertyName)
        {
            ObjectValidator.ValidateRequiredObject(value, propertyName);
            if (String.IsNullOrEmpty(value)) throw new ArgumentException($"{propertyName} may not be empty.", propertyName);
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException($"{propertyName} may not be whitespace.", propertyName);
        }

        public static void ValidateStringMaxLength(String value, String propertyName, Int32 maxLength)
        {
            if(value.Length > maxLength) throw new ArgumentOutOfRangeException(propertyName, $"{propertyName} must not exceed {maxLength} characters.");
        }

        public static void ValidateRequiredStringWithMaxLength(String value, String propertyName, Int32 maxLength)
        {
            StringValidator.ValidateRequiredString(value, propertyName);
            StringValidator.ValidateStringMaxLength(value, propertyName, maxLength);
        }
    }
}
