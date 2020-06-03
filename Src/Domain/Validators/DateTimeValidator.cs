using System;

namespace FactorioProductionCells.Domain.Validators
{
    public static class DateTimeValidator
    {
        public static void ValidateRequiredDateTime(DateTime value, String propertyName)
        {
            ObjectValidator.ValidateRequiredObject(value, propertyName);
        }

        public static void ValidateDateTimeBeforePresent(DateTime value, String propertyName)
        {
            if(value < DateTime.UtcNow) throw new ArgumentOutOfRangeException($"{propertyName} must be set to a time in the past.", propertyName);
        }

        public static void ValidateRequiredDateTimeBeforePresent(DateTime value, String propertyName)
        {
            DateTimeValidator.ValidateRequiredDateTime(value, propertyName);
            DateTimeValidator.ValidateDateTimeBeforePresent(value, propertyName);
        }
    }
}
