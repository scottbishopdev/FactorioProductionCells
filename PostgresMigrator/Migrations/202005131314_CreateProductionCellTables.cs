using FluentMigrator;

namespace Migrations
{
    [Migration(202005131314)]
    public class CreateProductionCellTables : Migration
    {
        public override void  Up()
        {
            Create.Table("ProductionCell")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("OwnerId").AsGuid().NotNullable().ForeignKey("UserAccount", "Id")
                .WithColumn("Title").AsString(200).NotNullable()
                .WithColumn("Description").AsString()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("UpdateDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("ProductionCellRecipe")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("ProductionCellId").AsGuid().NotNullable().ForeignKey("ProductionCell", "Id")
                .WithColumn("RecipeId").AsGuid().NotNullable().ForeignKey("Recipe", "Id")
                .WithColumn("MachineId").AsGuid().NotNullable().ForeignKey("Machine", "Id")
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("Sequence").AsInt32().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("UpdateDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("ProductionCellRecipeModule")
                .WithColumn("ProductionCellRecipeId").AsGuid().NotNullable().PrimaryKey().ForeignKey("ProductionCellRecipe", "Id")
                .WithColumn("ModuleId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Module", "Id")
                .WithColumn("Quantity").AsInt32().NotNullable();

            Create.Table("ProductionCellRecipeBeacon")
                .WithColumn("ProductionCellRecipeId").AsGuid().NotNullable().PrimaryKey().ForeignKey("ProductionCellRecipe", "Id")
                .WithColumn("ModuleId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Module", "Id")
                .WithColumn("BeaconQuantity").AsInt32().NotNullable()
                .WithColumn("ModuleQuantity").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ProductionCellRecipeBeacon");
            Delete.Table("ProductionCellRecipeModule");
            Delete.Table("ProductionCellRecipe");
            Delete.Table("ProductionCell");
        }
    }
}
