using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class FactorioVersion : ValueObject, IComparable
    {
        public const String FactorioVersionStringCapturePattern = @"^(\d+)\.(\d+)\$";

        private FactorioVersion() {}

        public static FactorioVersion For(String factorioVersionString)
        {
            factorioVersionString = factorioVersionString?.Trim();

            Regex factorioVersionStringCaptureRegex = new Regex(FactorioVersion.FactorioVersionStringCapturePattern);
            Match match = factorioVersionStringCaptureRegex.Match(factorioVersionString);            
            if (!match.Success) throw new ArgumentException($"Unable to parse \"{factorioVersionString}\" to a valid ReleaseFileName due to formatting.", "factorioVersionString");

            Int32 majorValue = Convert.ToInt32(match.Groups[0].Value);
            Int32 minorValue = Convert.ToInt32(match.Groups[1].Value);

            if (majorValue < 0 || minorValue < 0) throw new ArgumentOutOfRangeException($"Unable to parse \"{majorValue}.{minorValue}\" into a FactorioVersion - version parts must be positive.", "factorioVersionString");

            return new FactorioVersion
            {
                Major = majorValue,
                Minor = minorValue
            };
        }

        public Int32 Major { get; private set; }
        public Int32 Minor { get; private set; }

        public static implicit operator string(FactorioVersion version)
        {
            return version.ToString();
        }

        public static explicit operator FactorioVersion(string versionString)
        {
            return For(versionString);
        }

        public override bool Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a FactorioVersion.", "obj");
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(FactorioVersion left, FactorioVersion right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static bool operator !=(FactorioVersion left, FactorioVersion right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public static bool operator >(FactorioVersion left, FactorioVersion right)
        {
            return left.Major > right.Major
                || (left.Major == right.Major && left.Minor > left.Minor);
        }

        public static bool operator >=(FactorioVersion left, FactorioVersion right)
        {
            return left.Equals(right)
                || left.Major > right.Major
                || (left.Major == right.Major && left.Minor > left.Minor);
        }

        public static bool operator <(FactorioVersion left, FactorioVersion right)
        {
            return left.Major < right.Major
                || (left.Major == right.Major && left.Minor < left.Minor);
        }

        public static bool operator <=(FactorioVersion left, FactorioVersion right)
        {
            return left.Equals(right)
                || left.Major < right.Major
                || (left.Major == right.Major && left.Minor < left.Minor);
        }

        public int CompareTo(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("The specified object to compare is not a ModVersion.", "obj");

            FactorioVersion right = (FactorioVersion)obj;
            
            if (this < right) return -1;
            else if (this > right) return 1;
            else return 0;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Major;
            yield return Minor;
        }
    }
}
