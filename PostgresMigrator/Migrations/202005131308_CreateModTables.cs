using FluentMigrator;

namespace Migrations
{
    [Migration(202005131308)]
    public class CreateModTables : Migration
    {
        public override void  Up()
        {
            Create.Table("mod")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("name").AsString(200).NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("mod_translation")
                .WithColumn("mod_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("mod", "id")
                .WithColumn("language_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("language", "id")
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("mod_release")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("mod_id").AsGuid().NotNullable().ForeignKey("mod", "id")
                .WithColumn("sequence").AsInt32().NotNullable()
                .WithColumn("version").AsString().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("mod_release");
            Delete.Table("mod_translation");
            Delete.Table("mod");
        }
    }
}
