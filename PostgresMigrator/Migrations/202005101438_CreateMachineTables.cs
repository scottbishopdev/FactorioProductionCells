using FluentMigrator;

namespace Migrations
{
    [Migration(202005101438)]
    public class CreateMachineTables : Migration
    {
        public override void  Up()
        {
            Create.Table("machine")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("name").AsString(200).NotNullable()
                .WithColumn("crafting_speed").AsDouble().NotNullable()
                .WithColumn("module_slots").AsInt32().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("machine_translation")
                .WithColumn("machine_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("machine", "id")
                .WithColumn("language_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("language", "id")
                .WithColumn("display_name").AsString().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("machine_translation");
            Delete.Table("machine");
        }
    }
}