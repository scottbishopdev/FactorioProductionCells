using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Domain.Exceptions;

namespace FactorioProductionCells.Domain.ValueObjects
{
    public class ReleaseDownloadUrl : ValueObject
    {
        public const Int32 ReleaseTokenLength = 24;
        public static String ReleaseDownloadUrlStringCapturePattern = $"^\\/download\\/([\\S]{{1,{Mod.NameLength}}})\\/([0-9A-Fa-f]{{{ReleaseDownloadUrl.ReleaseTokenLength}}})$";
        
        private ReleaseDownloadUrl() {}

        public static ReleaseDownloadUrl For(string releaseDownloadUrlString)
        {
            releaseDownloadUrlString = releaseDownloadUrlString?.Trim();

            Regex releaseDownloadUrlStringCaptureRegex = new Regex(ReleaseDownloadUrl.ReleaseDownloadUrlStringCapturePattern);
            Match match = releaseDownloadUrlStringCaptureRegex.Match(releaseDownloadUrlString);            
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{releaseDownloadUrlString}\" to a valid ReleaseDownloadUrl due to formatting.", "releaseDownloadUrlString");

            return new ReleaseDownloadUrl
            {
                ModName = match.Groups[0].Value,
                ReleaseToken = match.Groups[1].Value
            };
        }

        public String ModName { get; private set; }
        public String ReleaseToken { get; private set; }

        public static implicit operator string(ReleaseDownloadUrl releaseDownloadUrl)
        {
            return releaseDownloadUrl.ToString();
        }

        public static explicit operator ReleaseDownloadUrl(string releaseDownloadUrlString)
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

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ModName;
            yield return ReleaseToken;
        }
    }
}
