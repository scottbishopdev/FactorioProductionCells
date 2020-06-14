using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ModVersion : ValueObject, IComparable
    {
        public const String ModVersionStringCapturePattern = @"^(\d+)\.(\d+)\.(\d+)$";
        
        private ModVersion() {}

        public static ModVersion For(String modVersionString)
        {
            modVersionString = modVersionString?.Trim();

            Regex modVersionStringCaptureRegex = new Regex(ModVersion.ModVersionStringCapturePattern);
            Match match = modVersionStringCaptureRegex.Match(modVersionString);            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{modVersionString}\" to a valid ReleaseFileName due to formatting.", "modVersionString");

            Int32 majorValue = Convert.ToInt32(match.Groups[0].Value);
            Int32 minorValue = Convert.ToInt32(match.Groups[1].Value);
            Int32 patchValue = Convert.ToInt32(match.Groups[2].Value);

            if (majorValue < 0 || minorValue < 0 || patchValue < 0) throw new ArgumentOutOfRangeException($"Unable to parse \"{majorValue}.{minorValue}\" into a ModVersion - version parts must be positive.", "factorioVersionString");

            return new ModVersion
            {
                Major = majorValue,
                Minor = minorValue,
                Patch = patchValue
            };
        }

        public Int32 Major { get; private set; }
        public Int32 Minor { get; private set; }
        public Int32 Patch { get; private set; }

        public static implicit operator string(ModVersion version)
        {
            return version.ToString();
        }

        public static explicit operator ModVersion(String versionString)
        {
            return For(versionString);
        }

        public override bool Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ModVersion.", "obj");
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ModVersion left, ModVersion right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static bool operator !=(ModVersion left, ModVersion right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public static bool operator >(ModVersion left, ModVersion right)
        {
            return left.Major > right.Major
                || (left.Major == right.Major && left.Minor > left.Minor)
                || (left.Major == right.Major && left.Minor == right.Minor && left.Patch > right.Patch);
        }

        public static bool operator >=(ModVersion left, ModVersion right)
        {
            return left.Equals(right)
                || left.Major > right.Major
                || (left.Major == right.Major && left.Minor > left.Minor)
                || (left.Major == right.Major && left.Minor == right.Minor && left.Patch > right.Patch);
        }

        public static bool operator <(ModVersion left, ModVersion right)
        {
            return left.Major < right.Major
                || (left.Major == right.Major && left.Minor < left.Minor)
                || (left.Major == right.Major && left.Minor == right.Minor && left.Patch < right.Patch);
        }

        public static bool operator <=(ModVersion left, ModVersion right)
        {
            return left.Equals(right)
                || left.Major < right.Major
                || (left.Major == right.Major && left.Minor < left.Minor)
                || (left.Major == right.Major && left.Minor == right.Minor && left.Patch < right.Patch);
        }

        public int CompareTo(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ModVersion.", "obj");

            ModVersion right = (ModVersion)obj;
            
            if (this < right) return -1;
            else if (this > right) return 1;
            else return 0;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Major;
            yield return Minor;
            yield return Patch;
        }
    }
}
