using FluentMigrator;

namespace Migrations
{
    [Migration(202005101754)]
    public class CreateRecipeTables : Migration
    {
        public override void  Up()
        {
            Create.Table("recipe")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("name").AsString(200).NotNullable()
                .WithColumn("crafting_time").AsDouble().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("recipe_translation")
                .WithColumn("recipe_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("recipe", "id")
                .WithColumn("language_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("language", "id")
                .WithColumn("display_name").AsString().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("recipe_ingredient")
                .WithColumn("recipe_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("recipe", "id")
                .WithColumn("item_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("item", "id")
                .WithColumn("quantity").AsInt32().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("recipe_product")
                .WithColumn("recipe_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("recipe", "id")
                .WithColumn("item_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("item", "id")
                .WithColumn("quantity").AsInt32().NotNullable()
                .WithColumn("probability").AsDouble().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
            
            Create.Table("recipe_valid_machines")
                .WithColumn("recipe_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("recipe", "id")
                .WithColumn("machine_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("machine", "id")
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("recipe_valid_machines");
            Delete.Table("recipe_product");
            Delete.Table("recipe_ingredient");
            Delete.Table("recipe_translation");
            Delete.Table("recipe");
        }
    }
}