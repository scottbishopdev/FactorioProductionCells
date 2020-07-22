using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ReleaseDownloadUrl : ValueObject
    {
        // Note: I'm curious if it'll be better to store this as the full character set like I'm doing now, or a regex character set (e.g. "[0-9A-Za-z%_\-]")
        public const String ValidModNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_%";
        public const Int32 ReleaseTokenLength = 24;
        public const String ValidReleaseTokenCharacters = "ABCDEFabcdef0123456789";
        public static String ReleaseDownloadUrlStringCapturePattern = @"^\/download\/([" + ValidModNameCharacters.Replace("-", "\\-") + @"]{1,})\/([" + ValidReleaseTokenCharacters + "]{1,})$";
        
        public static ReleaseDownloadUrl For(String releaseDownloadUrlString)
        {
            if (releaseDownloadUrlString == null) throw new ArgumentNullException("releaseDownloadUrlString", "A value for the release download URL must be provided.");

            Regex releaseDownloadUrlStringCaptureRegex = new Regex(ReleaseDownloadUrl.ReleaseDownloadUrlStringCapturePattern);
            Match match = releaseDownloadUrlStringCaptureRegex.Match(releaseDownloadUrlString.Trim());            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{releaseDownloadUrlString}\" to a valid ReleaseDownloadUrl due to formatting.", "releaseDownloadUrlString");

            String modName = match.Groups[1].Value;
            String releaseToken = match.Groups[2].Value;

            if (modName.Length > Mod.NameLength) throw new ArgumentException($"The mod name specified exceeds the maximum length of {Mod.NameLength}.", "releaseDownloadUrlString");
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

        public override bool Equals(Object obj)
        {
            if(obj.GetType() != this.GetType()) throw new ArgumentException("Unable to compare the specified object to a ReleaseDownloadUrl.", "obj");
            
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            return ValueObject.EqualOperator(left, right);
        }

        public static bool operator !=(ReleaseDownloadUrl left, ReleaseDownloadUrl right)
        {
            return ValueObject.NotEqualOperator(left, right);
        }

        public override string ToString()
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
