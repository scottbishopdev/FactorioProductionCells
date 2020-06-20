using System;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Domain.Entities
{
    public class DependencyComparisonType
    {
        public const Int32 NameLength = 25;
        
        private DependencyComparisonType() {}

        public DependencyComparisonType(DependencyComparisonTypeId enumId)
        {
            this.Id = enumId;
            this.Name = Enum.GetName(typeof(DependencyComparisonTypeId), enumId);
        }

        public DependencyComparisonType(int intId) : this((DependencyComparisonTypeId)intId) {}

        public static DependencyComparisonType For(String dependencyComparisonTypeString)
        {
            dependencyComparisonTypeString = dependencyComparisonTypeString?.Trim();

            switch (dependencyComparisonTypeString)
            {
                case ">":
                    return new DependencyComparisonType(DependencyComparisonTypeId.GreaterThan);
                case ">=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.GreaterThanOrEqualTo);
                case "=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.EqualTo);
                case "<":
                    return new DependencyComparisonType(DependencyComparisonTypeId.LessThan);
                case "<=":
                    return new DependencyComparisonType(DependencyComparisonTypeId.LessThanOrEqualTo);
                default:
                    throw new ArgumentException($"The specified string \"{dependencyComparisonTypeString}\" could not be parsed into a valid DependencyComparisonType."  , "dependencyComparisonTypeString");
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
                case DependencyComparisonTypeId.EqualTo:
                    return "=";
                case DependencyComparisonTypeId.LessThan:
                    return "<";
                case DependencyComparisonTypeId.LessThanOrEqualTo:
                    return "<=";
                default:
                    return "";
            }
        }
    }
}
