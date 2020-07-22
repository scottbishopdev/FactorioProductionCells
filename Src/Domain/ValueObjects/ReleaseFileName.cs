using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ReleaseFileName : ValueObject
    {
        public const String ValidModNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_ .";
        public static String ReleaseFileNameStringCapturePattern = @"^([" + ValidModNameCharacters.Replace("-", "\\-") + @"]{1,})_(-?\d+\.-?\d+\.-?\d+)\.zip$";

        private ReleaseFileName() {}

        public static ReleaseFileName For(String releaseFileNameString)
        {
            if (releaseFileNameString == null) throw new ArgumentNullException("releaseFileNameString", "A value for the release file name must be provided.");

            Regex releaseFileNameCaptureRegex = new Regex(ReleaseFileName.ReleaseFileNameStringCapturePattern);
            Match match = releaseFileNameCaptureRegex.Match(releaseFileNameString.Trim());            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{releaseFileNameString}\" to a valid ReleaseFileName due to formatting.", "releaseFileNameString");

            String modName = match.Groups[1].Value;
            String modVersionString = match.Groups[2].Value;

            if (modName.Length > Mod.NameLength) throw new ArgumentException($"The mod name specified exceeds the maximum length of {Mod.NameLength}.", "releaseFileNameString");

            return new ReleaseFileName
            {
                ModName = modName,
                ModVersion = ModVersion.For(modVersionString)
            };
        }

        public String ModName { get; private set; }
        public ModVersion ModVersion { get; private set; }

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
            return $"{ModName}_{ModVersion.ToString()}.zip";
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return ModName;
            yield return ModVersion;
        }
    }
}
