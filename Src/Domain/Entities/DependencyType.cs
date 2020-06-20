using System;
using FactorioProductionCells.Domain.Enums;

namespace FactorioProductionCells.Domain.Entities
{
    public class DependencyType
    {
        public const Int32 NameLength = 20;
        
        private DependencyType() {}

        public DependencyType(DependencyTypeId enumId)
        {
            this.Id = enumId;
            this.Name = Enum.GetName(typeof(DependencyTypeId), enumId);
        }

        public DependencyType(int intId) : this((DependencyTypeId)intId) {}

        public static DependencyType For(String dependencyTypeString)
        {
            dependencyTypeString = dependencyTypeString?.Trim();

            switch (dependencyTypeString)
            {
                case "!":
                    return new DependencyType(DependencyTypeId.Incompatibility);
                case "?":
                    return new DependencyType(DependencyTypeId.Optional);
                case "(?)":
                    return new DependencyType(DependencyTypeId.HiddenOptional);
                case "":
                    return new DependencyType(DependencyTypeId.Required);
                default:
                    throw new ArgumentException($"The specified string \"{dependencyTypeString}\" could not be parsed into a valid DependencyType."  , "dependencyTypeString");
            }
        }

        public DependencyTypeId Id { get; private set; }
        public String Name { get; private set; }

        public override String ToString()
        {
            switch (this.Id)
            {
                case DependencyTypeId.Incompatibility:
                    return "!";
                case DependencyTypeId.Optional:
                    return "?";
                case DependencyTypeId.HiddenOptional:
                    return "(?)";
                case DependencyTypeId.Required:
                    return "";
                default:
                    return "";
            }
        }
    }
}
