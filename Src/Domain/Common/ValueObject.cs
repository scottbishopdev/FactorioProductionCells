using System;
using System.Collections.Generic;
using System.Linq;

namespace FactorioProductionCells.Domain.Common
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left?.Equals(right) != false;
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public bool Equals(ValueObject other)
        {
            if (other == null || other.GetType() != GetType())
            {
                return false;
            }

            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }
        
        public override bool Equals(object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ValueObject.", "obj");

            return this.Equals((ValueObject)obj);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
