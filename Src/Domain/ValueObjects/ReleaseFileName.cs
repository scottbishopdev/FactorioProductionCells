using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ReleaseFileName : ValueObject
    {
        public static String ReleaseFileNameStringCapturePattern = @"^([\S]{1," + Mod.NameLength.ToString() + @"})_([0-9]+\.[0-9]+\.[0-9]+)\.zip$";

        private ReleaseFileName() {}

        public static ReleaseFileName For(string releaseFileNameString)
        {
            releaseFileNameString = releaseFileNameString?.Trim();

            Regex releaseFileNameCaptureRegex = new Regex(ReleaseFileName.ReleaseFileNameStringCapturePattern);
            Match match = releaseFileNameCaptureRegex.Match(releaseFileNameString);            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{releaseFileNameString}\" to a valid ReleaseFileName due to formatting.", "releaseFileNameString");

            return new ReleaseFileName
            {
                ModName = match.Groups[0].Value,
                Version = ModVersion.For(match.Groups[1].Value)
            };
        }

        public String ModName { get; private set; }
        public ModVersion Version { get; private set; }

        public static implicit operator string(ReleaseFileName releaseFileName)
        {
            return releaseFileName.ToString();
        }

        public static explicit operator ReleaseFileName(string releaseFileNameString)
        {
            return For(releaseFileNameString);
        }

        public override bool Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ReleaseFileName.", "obj");

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ReleaseFileName left, ReleaseFileName right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static bool operator !=(ReleaseFileName left, ReleaseFileName right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public override string ToString()
        {
            return $"{ModName}_{Version.ToString()}.zip";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ModName;
            yield return Version;
        }
    }
}
