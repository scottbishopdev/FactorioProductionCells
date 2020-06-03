using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ModVersion : ValueObject
    {
        public const string ModVersionStringRegex = @"^\d+\.\d+\.\d+$";
        private ModVersion() {}

        public static ModVersion For(string versionString)
        {
            versionString = versionString?.Trim();
            Regex modVersionPattern = new Regex(ModVersion.ModVersionStringRegex);
            if (!modVersionPattern.IsMatch(versionString)) throw new InvalidModVersionException($"Unable to parse \"{versionString}\" to a valid ModVersion due to formatting.");
            
            var modVersion = new ModVersion();

            try
            {
                var intArray = versionString.Split('.').Select(n => Convert.ToInt32(n)).ToArray();
                modVersion.Major = intArray[0];
                modVersion.Minor = intArray[1];
                modVersion.Patch = intArray[2];
            }
            catch (Exception ex)
            {
                throw new InvalidModVersionException($"An error occurred while attempting to parse the string \"{versionString}\" into a ModVersion.", ex);
            }

            return modVersion;
        }

        public Int32 Major { get; private set; }
        public Int32 Minor { get; private set; }
        public Int32 Patch { get; private set; }

        public static implicit operator string(ModVersion version)
        {
            return version.ToString();
        }

        public static explicit operator ModVersion(string versionString)
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
                ModVersion right = (ModVersion) obj;
                return this.Major == right.Major
                    && this.Minor == right.Minor
                    && this.Patch == right.Patch;
            }
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(this.Major);
            hash.Add(this.Minor);
            hash.Add(this.Patch);
            return hash.ToHashCode();
        }

        public static bool operator ==(ModVersion left, ModVersion right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ModVersion left, ModVersion right)
        {
            return !left.Equals(right);
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
