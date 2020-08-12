using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ReleaseFileName : ValueObject
    {
        public static String ReleaseFileNameStringCapturePattern = @"^([" + Regex.Escape(Mod.ValidModNameCharacters) + @"]+)_(-?\d+\.-?\d+\.-?\d+)\.zip$";

        private ReleaseFileName() {}

        public ReleaseFileName(ReleaseFileName original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));
            
            this.ModName = original.ModName;
            this.ModVersion = original.ModVersion != null ? new ModVersion(original.ModVersion) : null;
        }

        public static ReleaseFileName For(String releaseFileNameString)
        {
            StringValidator.ValidateRequiredString(releaseFileNameString, nameof(releaseFileNameString));

            Regex releaseFileNameCaptureRegex = new Regex(ReleaseFileName.ReleaseFileNameStringCapturePattern);
            Match match = releaseFileNameCaptureRegex.Match(releaseFileNameString);
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

        public Boolean Equals(ReleaseFileName right)
        {
            return base.Equals(right);
        }

        public override Boolean Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ReleaseFileName.", "obj");

            return base.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Boolean operator ==(ReleaseFileName left, ReleaseFileName right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static Boolean operator !=(ReleaseFileName left, ReleaseFileName right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public override String ToString()
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
