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
        //public const String DependencyStringCapturePattern = @"^(\?|!|\(\?\))? ?([\S]{1,}) (>=|>|=|<=|<) ([0-9]+\.[0-9]+\.[0-9]+)$";
        public static String DependencyStringCapturePattern = $"^(\\?|!|\\(\\?\\))? ?([\\S]{{1,{Mod.NameLength}}}) (>=|>|=|<=|<) ([0-9]+\\.[0-9]+\\.[0-9]+)$";
        
        private Dependency() {}

        //public Dependency(Guid ReleaseId, /*Guid DependentModId*/ String DependentModName, DependencyType DependencyType, DependencyComparisonType DependencyComparisonType, ModVersion DependentModVersion)
        public Dependency(Release Release, /*Guid DependentModId*/ String DependentModName, DependencyType DependencyType, DependencyComparisonType DependencyComparisonType, ModVersion DependentModVersion)
        {
            //ObjectValidator.ValidateRequiredObject(ReleaseId, nameof(ReleaseId));
            ObjectValidator.ValidateRequiredObject(Release, nameof(Release));
            ObjectValidator.ValidateRequiredObject(DependencyType, nameof(DependencyType));
            //ObjectValidator.ValidateRequiredObject(DependentModId, nameof(DependentModId));
            StringValidator.ValidateRequiredStringWithMaxLength(DependentModName, nameof(DependentModName), Mod.NameLength);
            ObjectValidator.ValidateRequiredObject(DependencyComparisonType, nameof(DependencyComparisonType));
            ObjectValidator.ValidateRequiredObject(DependentModVersion, nameof(DependentModVersion));

            //this.ReleaseId = ReleaseId;
            this.Release = Release;
            this.DependencyType = DependencyType;
            //this.DependentModId = DependentModId;
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

            String dependencyTypeValue = match.Groups[0].Value;
            String dependentModNameValue = match.Groups[1].Value;
            String dependencyComparisonTypeValue = match.Groups[2].Value;
            String modVersionValue = match.Groups[3].Value;

            // TODO: Implement a converter that converts the stored mod name into a ModId so we can store a FK in the database.
            return new Dependency
            {
                DependencyType = DependencyType.For(dependencyTypeValue),
                DependentModName = dependentModNameValue,
                DependencyComparisonType = DependencyComparisonType.For(dependencyComparisonTypeValue),
                DependentModVersion = ModVersion.For(modVersionValue)
            };
        }

        public Guid ReleaseId { get; private set; }
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
