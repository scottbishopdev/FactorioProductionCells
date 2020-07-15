using System;

namespace FactorioProductionCells.Domain.Validators
{
    //TODO: Look into Fluent Validation and see if these are already implemented there, or if they should be implemented using it. (IValidator?)
    public static class DateTimeValidator
    {
        public static void ValidateRequiredDateTime(DateTime value, String propertyName)
        {
            ObjectValidator.ValidateRequiredObject(value, propertyName);
        }

        public static void ValidateDateTimeBeforePresent(DateTime value, String propertyName)
        {
            // TODO: Validating the time this way introduces a dependency on the system time, but I'm not sure how to get around this since this validation lives in the 
            // domain layer and the interface for our IDateTimeService is down in application. Can we move some interfaces up to Domain? Does that make sense to do?
            if(value > DateTime.UtcNow) throw new ArgumentOutOfRangeException(propertyName, $"{propertyName} must be set to a time in the past.");
        }

        public static void ValidateRequiredDateTimeBeforePresent(DateTime value, String propertyName)
        {
            DateTimeValidator.ValidateRequiredDateTime(value, propertyName);
            DateTimeValidator.ValidateDateTimeBeforePresent(value, propertyName);
        }
    }
}
