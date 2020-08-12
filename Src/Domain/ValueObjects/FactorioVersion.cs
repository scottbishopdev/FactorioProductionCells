using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class FactorioVersion : ValueObject, IComparable, IEquatable<FactorioVersion>
    {
        public const String FactorioVersionStringCapturePattern = @"^(-?\d+)\.(-?\d+)$";

        private FactorioVersion() {}

        public FactorioVersion(FactorioVersion original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));

            this.Major = original.Major;
            this.Minor = original.Minor;
        }

        public static FactorioVersion For(String factorioVersionString)
        {
            StringValidator.ValidateRequiredString(factorioVersionString, nameof(factorioVersionString));

            Regex factorioVersionStringCaptureRegex = new Regex(FactorioVersion.FactorioVersionStringCapturePattern);
            Match match = factorioVersionStringCaptureRegex.Match(factorioVersionString);            
            if (!match.Success) throw new ArgumentException($"Unable to parse \"{factorioVersionString}\" to a valid FactorioVersion due to formatting.", "factorioVersionString");

            Int32 majorValue = Convert.ToInt32(match.Groups[1].Value);
            Int32 minorValue = Convert.ToInt32(match.Groups[2].Value);

            if (majorValue < 0 || minorValue < 0) throw new ArgumentOutOfRangeException( "factorioVersionString", $"Unable to parse \"{factorioVersionString}\" into a FactorioVersion - version parts must be positive.");

            return new FactorioVersion
            {
                Major = majorValue,
                Minor = minorValue
            };
        }

        public Int32 Major { get; private set; }
        public Int32 Minor { get; private set; }

        public static implicit operator String(FactorioVersion version)
        {
            return version.ToString();
        }

        public static explicit operator FactorioVersion(String versionString)
        {
            return For(versionString);
        }

        public Boolean Equals(FactorioVersion right)
        {
            return base.Equals(right);
        }

        public override Boolean Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a FactorioVersion.", "obj");
            
            return base.Equals(obj);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public static Boolean operator ==(FactorioVersion left, FactorioVersion right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static Boolean operator !=(FactorioVersion left, FactorioVersion right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public static Boolean operator >(FactorioVersion left, FactorioVersion right)
        {
            return left.Major > right.Major
                || (left.Major == right.Major && left.Minor > right.Minor);
        }

        public static Boolean operator >=(FactorioVersion left, FactorioVersion right)
        {
            return left.Equals(right)
                || left.Major > right.Major
                || (left.Major == right.Major && left.Minor > right.Minor);
        }

        public static Boolean operator <(FactorioVersion left, FactorioVersion right)
        {
            return left.Major < right.Major
                || (left.Major == right.Major && left.Minor < right.Minor);
        }

        public static Boolean operator <=(FactorioVersion left, FactorioVersion right)
        {
            return left.Equals(right)
                || left.Major < right.Major
                || (left.Major == right.Major && left.Minor < right.Minor);
        }

        public Int32 CompareTo(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("The specified object to compare is not a FactorioVersion.", "obj");

            FactorioVersion right = (FactorioVersion)obj;
            
            if (this < right) return -1;
            else if (this > right) return 1;
            else return 0;
        }

        public override String ToString()
        {
            return $"{Major}.{Minor}";
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Major;
            yield return Minor;
        }
    }
}
