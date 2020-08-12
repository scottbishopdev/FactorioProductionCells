using System;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class DependencyComparisonType : IEquatable<DependencyComparisonType>
    {
        public const Int32 NameLength = 25;
        
        private DependencyComparisonType() {}

        public DependencyComparisonType(DependencyComparisonType original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));
            
            this.Id = original.Id;
            this.Name = original.Name;
        }

        public DependencyComparisonType(DependencyComparisonTypeId enumId)
        {
            this.Id = enumId;
            this.Name = Enum.GetName(typeof(DependencyComparisonTypeId), enumId);
        }

        public DependencyComparisonType(int intId)
        {
            if(!Enum.IsDefined(typeof(DependencyComparisonTypeId), intId)) throw new ArgumentOutOfRangeException("intId", $"Unable to parse the supplied id {intId} into a DependencyComparisonType.");

            this.Id = (DependencyComparisonTypeId)intId;
            this.Name = Enum.GetName(typeof(DependencyComparisonTypeId), (DependencyComparisonTypeId)intId);
        }

        public static DependencyComparisonType For(String dependencyComparisonTypeString)
        {
            StringValidator.ValidateRequiredString(dependencyComparisonTypeString, nameof(dependencyComparisonTypeString));
            
            switch (dependencyComparisonTypeString)
            {
                case ">":
                    return new DependencyComparisonType(DependencyComparisonTypeId.GreaterThan);
                case ">=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo);
                case "<":
                    return new DependencyComparisonType(DependencyComparisonTypeId.LessThan);
                case "<=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.LessThanOrEqualTo);
                case "=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.EqualTo);
                case "!=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.NotEqualTo);
                default:
                    throw new ArgumentException($"The specified string \"{dependencyComparisonTypeString}\" could not be parsed into a valid DependencyComparisonType.", "dependencyComparisonTypeString");
            }
        }

        public DependencyComparisonTypeId Id { get; private set; }
        public String Name { get; private set; }

        public override String ToString()
        {
            switch (this.Id)
            {
                case DependencyComparisonTypeId.GreaterThan:
                    return ">";
                case DependencyComparisonTypeId.GreaterThanOrEqualTo:
                    return ">=";
                case DependencyComparisonTypeId.LessThan:
                    return "<";
                case DependencyComparisonTypeId.LessThanOrEqualTo:
                    return "<=";
                case DependencyComparisonTypeId.EqualTo:
                    return "=";
                case DependencyComparisonTypeId.NotEqualTo:
                    return "!=";
                default:
                    return "";
            }
        }

        public Boolean Equals(DependencyComparisonType right)
        {
            return right != null
                && this.Id == right.Id
                && ((this.Name == null && right.Name == null) || this.Name == right.Name);
        }
    }
}
