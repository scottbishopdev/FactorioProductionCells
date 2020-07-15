using System;
using System.Text.RegularExpressions;
using FactorioProductionCells.Domain.Common;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;
using FactorioProductionCells.Domain.ValueObjects;

namespace FactorioProductionCells.Domain.Entities
{
    public class Dependency : AuditableEntity
    {
        public static String DependencyStringCapturePattern = $"^(\\?|!|\\(\\?\\))? ?([\\S]{{1,{Mod.NameLength}}}) (>=|>|=|<=|<) ([0-9]+\\.[0-9]+\\.[0-9]+)$";
        
        private Dependency() {}

        public Dependency(Release Release, String DependentModName, DependencyType DependencyType, DependencyComparisonType DependencyComparisonType, ModVersion DependentModVersion)
        {
            ObjectValidator.ValidateRequiredObject(Release, nameof(Release));
            ObjectValidator.ValidateRequiredObject(DependencyType, nameof(DependencyType));
            StringValidator.ValidateRequiredStringWithMaxLength(DependentModName, nameof(DependentModName), Mod.NameLength);
            ObjectValidator.ValidateRequiredObject(DependencyComparisonType, nameof(DependencyComparisonType));
            ObjectValidator.ValidateRequiredObject(DependentModVersion, nameof(DependentModVersion));

            this.Release = Release;
            this.DependencyType = DependencyType;
            this.DependentModName = DependentModName;
            this.DependencyComparisonType = DependencyComparisonType;
            this.DependentModVersion = DependentModVersion;
        }

        public static Dependency For(String dependencyString)
        {
            dependencyString = dependencyString?.Trim();

            Regex dependencyStringCaptureRegex = new Regex(Dependency.DependencyStringCapturePattern);
            Match match = dependencyStringCaptureRegex.Match(dependencyString);
            if(!match.Success) throw new ArgumentException($"Unable to parse \"{dependencyString}\" to a valid Dependency due to formatting.", "dependencyString");

            String dependencyTypeValue = match.Groups[1].Value;
            String dependentModNameValue = match.Groups[2].Value;
            String dependencyComparisonTypeValue = match.Groups[3].Value;
            String modVersionValue = match.Groups[4].Value;

            return new Dependency
            {
                DependencyType = DependencyType.For(dependencyTypeValue),
                DependentModName = dependentModNameValue,
                DependencyComparisonType = DependencyComparisonType.For(dependencyComparisonTypeValue),
                DependentModVersion = ModVersion.For(modVersionValue)
            };
        }

        public Guid ReleaseId { get; private set; }
        // TODO: Would it be possible to link directly to the release we're dependent on here instead of just a mod? If so, I'd imagine we'd need to validate the hell out of that relationship.
        public Guid DependentModId { get; private set; }
        public DependencyTypeId DependencyTypeId { get; private set; }
        public String DependentModName { get; private set; }
        public DependencyComparisonTypeId DependencyComparisonTypeId { get; private set; }
        public ModVersion DependentModVersion { get; private set; }

        // Navigation properties
        public Release Release { get; private set; }
        public Mod DependentMod { get; private set; }
        public DependencyType DependencyType { get; private set; }
        public DependencyComparisonType DependencyComparisonType { get; private set; }

        public override String ToString()
        {
            if (this.DependencyType.Id == DependencyTypeId.Required)
            {
                return $"{DependentModName} {DependencyComparisonType.ToString()} {DependentModVersion.ToString()}";
            }
            else
            {
                return $"{DependencyType.ToString()} {DependentModName} {DependencyComparisonType.ToString()} {DependentModVersion.ToString()}";
            }
        }
    }
}
