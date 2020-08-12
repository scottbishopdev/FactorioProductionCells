using System;
using System.Text;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Dependency : AuditableEntity, IEquatable<Dependency>
    {
        public static String DependencyStringCapturePattern = @"^(?!\ )(\?|!|\(\?\))? ?([" + Regex.Escape(Mod.ValidModNameCharacters) + @"]+?)(?:(?: ?)(>=|>|<=|<|=|!=){1} ?(-?\d+\.-?\d+(?:\.-?\d+)?){1})?$";

        private Dependency() {}

        public Dependency(Dependency original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));

            // Properties
            this.DependencyType = original.DependencyType != null ? new DependencyType(original.DependencyType) : null;
            if (original.DependentModName != null) this.DependentModName = original.DependentModName;
            this.DependencyComparisonType = original.DependencyComparisonType != null ? new DependencyComparisonType(original.DependencyComparisonType) : null;
            this.DependentModVersion = original.DependentModVersion != null ? new ModVersion(original.DependentModVersion) : null;
            
            // Navigation Properties
            this.DependencyTypeId = original.DependencyTypeId;
            this.DependencyComparisonTypeId = original.DependencyComparisonTypeId;
            this.DependentMod = original.DependentMod != null ? new Mod(original.DependentMod) : null;
            if (original.DependentModId != null) this.DependentModId = original.DependentModId;
            this.Release = original.Release != null ? new Release(original.Release) : null;
            if (original.ReleaseId != null) this.ReleaseId = original.ReleaseId;
        }

        public Dependency(DependencyType DependencyType, String DependentModName, DependencyComparisonType DependencyComparisonType, ModVersion DependentModVersion)
        {
            ObjectValidator.ValidateRequiredObject(DependencyType, nameof(DependencyType));
            StringValidator.ValidateRequiredStringWithMaxLength(DependentModName, nameof(DependentModName), Mod.NameLength);

            this.DependencyType = DependencyType;
            this.DependentModName = DependentModName;
            this.DependencyComparisonType = DependencyComparisonType != null ? new DependencyComparisonType(DependencyComparisonType) : null;
            this.DependentModVersion = DependentModVersion != null ? new ModVersion(DependentModVersion) : null;
        }

        public static Dependency For(String dependencyString)
        {
            StringValidator.ValidateRequiredString(dependencyString, nameof(dependencyString));
            
            Regex dependencyStringCaptureRegex = new Regex(Dependency.DependencyStringCapturePattern);
            Match match = dependencyStringCaptureRegex.Match(dependencyString);
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{dependencyString}\" to a valid Dependency due to formatting.", "dependencyString");

            String dependencyTypeValue = match.Groups[1].Value;
            String dependentModNameValue = match.Groups[2].Value;
            String dependencyComparisonTypeValue = match.Groups[3].Value;
            String modVersionValue = match.Groups[4].Value;

            if (dependentModNameValue.Length > Mod.NameLength) throw new ArgumentException($"The mod name specified exceeds the maximum length of {Mod.NameLength}.", "dependencyString");

            Regex modVersionFormatCheckRegex = new Regex(FactorioVersion.FactorioVersionStringCapturePattern);
            Match modVersionFormatCheckMatch = modVersionFormatCheckRegex.Match(modVersionValue);
            // TODO: We may want to throw a specific exception if we get a ModVersion in the format of a FactorioVersion when the ModName isn't "base".
            if(dependentModNameValue == "base" && modVersionFormatCheckMatch.Success)
            {
                modVersionValue += @".0";
            }

            return new Dependency
            {
                DependencyType = dependencyTypeValue == null ? DependencyType.For("") : DependencyType.For(dependencyTypeValue),
                DependentModName = dependentModNameValue,
                DependencyComparisonType = (String.IsNullOrEmpty(dependencyComparisonTypeValue)) ?  null : DependencyComparisonType.For(dependencyComparisonTypeValue),
                DependentModVersion = (String.IsNullOrEmpty(modVersionValue)) ?  null : ModVersion.For(modVersionValue)
            };
        }

        public Guid? ReleaseId { get; private set; }
        // TODO: Would it be possible to link directly to the release we're dependent on here instead of just a mod? If so, I'd imagine we'd need to validate the hell out of that relationship.
        public Guid DependentModId { get; private set; }
        public DependencyTypeId DependencyTypeId { get; private set; }
        public String DependentModName { get; private set; }
        public DependencyComparisonTypeId? DependencyComparisonTypeId { get; private set; }
        public ModVersion DependentModVersion { get; private set; }

        // Navigation properties
        public Release Release { get; private set; }
        public Mod DependentMod { get; private set; }
        public DependencyType DependencyType { get; private set; }
        public DependencyComparisonType DependencyComparisonType { get; private set; }

        public override String ToString()
        {
            var resultStringBuilder = new StringBuilder();

            if (this.DependencyType != null)
            {
                resultStringBuilder.Append(this.DependencyType);
                if (this.DependencyType.Id != DependencyTypeId.Required) resultStringBuilder.Append(" ");
            }

            resultStringBuilder.Append(this.DependentModName);

            if (this.DependencyComparisonType != null)
            {
                resultStringBuilder.Append(" ");
                resultStringBuilder.Append(this.DependencyComparisonType);
                resultStringBuilder.Append(" ");
                resultStringBuilder.Append(this.DependentModVersion);
            }

            return resultStringBuilder.ToString();
        }

        public Boolean Equals(Dependency right)
        {
            return right != null
                && ((this.ReleaseId == null && right.ReleaseId == null) || this.ReleaseId == right.ReleaseId)
                && ((this.DependentModId == null && right.DependentModId == null) || this.DependentModId == right.DependentModId)
                && this.DependencyTypeId == right.DependencyTypeId
                && ((this.DependentModName == null && right.DependentModName == null) || this.DependentModName == right.DependentModName)
                && this.DependencyComparisonTypeId == right.DependencyComparisonTypeId
                && ((this.DependentModVersion == null && right.DependentModVersion == null) || this.DependentModVersion == right.DependentModVersion)
                && ((this.Release == null && right.Release == null) || this.Release.Equals(right.Release))
                && ((this.DependentMod == null && right.DependentMod == null) || this.DependentMod.Equals(right.DependentMod))
                && ((this.DependencyType == null && right.DependencyType == null) || this.DependencyType.Equals(right.DependencyType))
                && ((this.DependencyComparisonType == null && right.DependencyComparisonType == null) || this.DependencyComparisonType.Equals(right.DependencyComparisonType));
        }
    }
}
