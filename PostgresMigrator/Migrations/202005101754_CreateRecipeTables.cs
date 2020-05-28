using FluentMigrator;

namespace Migrations
{
    [Migration(202005101754)]
    public class CreateRecipeTables : Migration
    {
        public override void  Up()
        {
            Create.Table("Recipe")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("CraftingTime").AsDouble().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("RecipeTranslation")
                .WithColumn("RecipeId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Recipe", "Id")
                .WithColumn("LanguageId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Language", "Id")
                .WithColumn("DisplayName").AsString().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("RecipeIngredient")
                .WithColumn("RecipeId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Recipe", "Id")
                .WithColumn("ItemId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Item", "Id")
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("RecipeProduct")
                .WithColumn("RecipeId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Recipe", "Id")
                .WithColumn("ItemId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Item", "Id")
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("Probability").AsDouble().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
            
            Create.Table("RecipeValidMachines")
                .WithColumn("RecipeId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Recipe", "Id")
                .WithColumn("MachineId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Machine", "Id")
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("RecipeValidMachines");
            Delete.Table("RecipeProduct");
            Delete.Table("RecipeIngredient");
            Delete.Table("RecipeTranslation");
            Delete.Table("Recipe");
        }
    }
}