using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class FactorioVersion : ValueObject
    {
        public const string FactorioVersionStringRegex = @"^\d+\.\d+\$";
        private FactorioVersion() {}

        public static FactorioVersion For(string versionString)
        {
            versionString = versionString?.Trim();
            Regex factorioVersionPattern = new Regex(FactorioVersion.FactorioVersionStringRegex);
            if (!factorioVersionPattern.IsMatch(versionString)) throw new InvalidFactorioVersionException($"Unable to parse \"{versionString}\" to a valid FactorioVersion due to formatting.");
            
            var factorioVersion = new FactorioVersion();

            try
            {
                var intArray = versionString.Split('.').Select(n => Convert.ToInt32(n)).ToArray();
                factorioVersion.Major = intArray[0];
                factorioVersion.Minor = intArray[1];
            }
            catch (Exception ex)
            {
                throw new InvalidFactorioVersionException($"An error occurred while attempting to parse the string \"{versionString}\" into a FactorioVersion.", ex);
            }

            return factorioVersion;
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
            if(obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                FactorioVersion right = (FactorioVersion) obj;
                return this.Major == right.Major
                    && this.Minor == right.Minor;
            }
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Major);
            hash.Add(this.Minor);
            return hash.ToHashCode();
        }

        public static bool operator ==(FactorioVersion left, FactorioVersion right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FactorioVersion left, FactorioVersion right)
        {
            return !left.Equals(right);
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
