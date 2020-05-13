using FluentMigrator;

namespace Migrations
{
    [Migration(202005111146)]
    public class CreateModulesTables : Migration
    {
        public override void  Up()
        {
            Create.Table("module")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("name").AsString(200).NotNullable()
                .WithColumn("productivity_bonus").AsDouble().NotNullable()
                .WithColumn("speed_bonus").AsDouble().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("module_translation")
                .WithColumn("module_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("module", "id")
                .WithColumn("language_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("language", "id")
                .WithColumn("display_name").AsString().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("module_translation");
            Delete.Table("module");
        }
    }
}