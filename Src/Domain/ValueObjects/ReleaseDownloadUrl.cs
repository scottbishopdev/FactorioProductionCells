using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ReleaseDownloadUrl : ValueObject, IEquatable<ReleaseDownloadUrl>
    {
        public const Int32 ReleaseTokenLength = 24;
        public const String ValidReleaseTokenCharacters = "ABCDEFabcdef0123456789";
        public static String ReleaseDownloadUrlStringCapturePattern = @"^\/download\/((?:[" + Regex.Escape(Mod.ValidModNameCharacters.Replace(" ", "")) + @"]|%20)+)\/([" + Regex.Escape(ValidReleaseTokenCharacters) + "]+)$";

        private ReleaseDownloadUrl() {}

        public ReleaseDownloadUrl(ReleaseDownloadUrl original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));

            this.ModName = original.ModName;
            this.ReleaseToken = original.ReleaseToken;
        }

        public static ReleaseDownloadUrl For(String releaseDownloadUrlString)
        {
            StringValidator.ValidateRequiredString(releaseDownloadUrlString, nameof(releaseDownloadUrlString));

            Regex releaseDownloadUrlStringCaptureRegex = new Regex(ReleaseDownloadUrl.ReleaseDownloadUrlStringCapturePattern);
            Match match = releaseDownloadUrlStringCaptureRegex.Match(releaseDownloadUrlString);            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{releaseDownloadUrlString}\" to a valid ReleaseDownloadUrl due to formatting.", "releaseDownloadUrlString");

            String urlModName = match.Groups[1].Value;
            String releaseToken = match.Groups[2].Value;

            if (urlModName.Replace("%20", " ").Length > Mod.NameLength) throw new ArgumentException($"The mod name specified exceeds the maximum length of {Mod.NameLength}.", "releaseDownloadUrlString");
            if (releaseToken.Length > ReleaseDownloadUrl.ReleaseTokenLength) throw new ArgumentException($"The release token specified exceeds the maximum length of {ReleaseDownloadUrl.ReleaseTokenLength}.", "releaseDownloadUrlString");

            return new ReleaseDownloadUrl
            {
                ModName = match.Groups[1].Value,
                ReleaseToken = match.Groups[2].Value
            };
        }


        public String ModName { get; private set; }
        public String ReleaseToken { get; private set; }

        public static implicit operator String(ReleaseDownloadUrl releaseDownloadUrl)
        {
            return releaseDownloadUrl.ToString();
        }

        public static explicit operator ReleaseDownloadUrl(String releaseDownloadUrlString)
        {
            return For(releaseDownloadUrlString);
        }

        public Boolean Equals(ReleaseDownloadUrl right)
        {
            return base.Equals(right);
        }

        public override Boolean Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ReleaseDownloadUrl.", "obj");
            
            return base.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Boolean operator ==(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static Boolean operator !=(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public override String ToString()
        {
            return $"/download/{ModName}/{ReleaseToken}";
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return ModName;
            yield return ReleaseToken;
        }
    }
}
