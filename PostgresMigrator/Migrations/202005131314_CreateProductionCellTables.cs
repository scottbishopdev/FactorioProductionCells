using FluentMigrator;

namespace Migrations
{
    [Migration(202005131314)]
    public class CreateProductionCellTables : Migration
    {
        public override void  Up()
        {
            Create.Table("production_cell")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("owner_id").AsGuid().NotNullable().ForeignKey("user_account", "id")
                .WithColumn("title").AsString(200).NotNullable()
                .WithColumn("description").AsString()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("production_cell_recipe")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("production_cell_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("production_cell", "id")
                .WithColumn("recipe_id").AsGuid().NotNullable().ForeignKey("recipe", "id")
                .WithColumn("machine_id").AsGuid().NotNullable().ForeignKey("machine", "id")
                .WithColumn("quantity").AsInt32().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("production_cell_recipe_module")
                .WithColumn("production_cell_recipe_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("production_cell_recipe", "id")
                .WithColumn("module_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("module", "id")
                .WithColumn("quantity").AsInt32().NotNullable();

            Create.Table("production_cell_recipe_beacon")
                .WithColumn("production_cell_recipe_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("production_cell_recipe", "id")
                .WithColumn("module_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("module", "id")
                .WithColumn("beacon_quantity").AsInt32().NotNullable()
                .WithColumn("module_quantity").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("production_cell_recipe_beacon");
            Delete.Table("production_cell_recipe_module");
            Delete.Table("production_cell_recipe");
            Delete.Table("production_cell");
        }
    }
}
