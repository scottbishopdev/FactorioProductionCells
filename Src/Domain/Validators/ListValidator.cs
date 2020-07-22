using System;
using System.Collections.Generic;
using System.Linq;

namespace FactorioProductionCells.Domain.Validators
{
    public static class ListValidator
    {
        public static void ValidateRequiredList<T>(IList<T> value, String propertyName)
        {
            ObjectValidator.ValidateRequiredObject(value, propertyName);
        }

        public static void ValidateListNotEmpty<T>(IList<T> value, String propertyName)
        {
            //if(!value.Any()) throw new ArgumentException($"{propertyName} must contain at least one entry.", propertyName);
            if(value.Count == 0) throw new ArgumentException($"{propertyName} must contain at least one entry.", propertyName);
        }

        public static void ValidateRequiredListNotEmpty<T>(IList<T> value, String propertyName)
        {
            ListValidator.ValidateRequiredList<T>(value, propertyName);
            ListValidator.ValidateListNotEmpty<T>(value, propertyName);
        }
    }
}
