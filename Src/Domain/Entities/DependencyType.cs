using System;
using FactorioProductionCells.Domain.Enums;
using FactorioProductionCells.Domain.Validators;

namespace FactorioProductionCells.Domain.Entities
{
    public class DependencyType : IEquatable<DependencyType>
    {
        public const Int32 NameLength = 20;
        
        private DependencyType() {}

        public DependencyType(DependencyType original)
        {
            ObjectValidator.ValidateRequiredObject(original, nameof(original));
            
            this.Id = original.Id;
            this.Name = original.Name;
        }

        public DependencyType(DependencyTypeId enumId)
        {
            this.Id = enumId;
            this.Name = Enum.GetName(typeof(DependencyTypeId), enumId);
        }

        public DependencyType(Int32 intId)
        {
            if(!Enum.IsDefined(typeof(DependencyTypeId), intId)) throw new ArgumentOutOfRangeException("intId", $"Unable to parse the supplied id {intId} into a DependencyType.");

            this.Id = (DependencyTypeId)intId;
            this.Name = Enum.GetName(typeof(DependencyTypeId), (DependencyTypeId)intId);
        }

        public static DependencyType For(String dependencyTypeString)
        {
            ObjectValidator.ValidateRequiredObject(dependencyTypeString, nameof(dependencyTypeString));
            if (String.IsNullOrWhiteSpace(dependencyTypeString) && dependencyTypeString != "") throw new ArgumentException($"dependencyTypeString may not be whitespace.", "dependencyTypeString");

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
                    throw new ArgumentException($"The specified string \"{dependencyTypeString}\" could not be parsed into a valid DependencyType.", "dependencyTypeString");
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

        public Boolean Equals(DependencyType right)
        {
            return right != null
                && this.Id == right.Id
                && ((this.Name == null && right.Name == null) || this.Name == right.Name);
        }
    }
}
