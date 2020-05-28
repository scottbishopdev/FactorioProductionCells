using FluentMigrator;

namespace Migrations
{
    [Migration(202005131308)]
    public class CreateModTables : Migration
    {
        public override void  Up()
        {
            Create.Table("Mod")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("UpdateDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("ModTranslation")
                .WithColumn("ModId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Mod", "Id")
                .WithColumn("LanguageId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Language", "Id")
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("ModRelease")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("ModId").AsGuid().NotNullable().ForeignKey("Mod", "Id")
                .WithColumn("Sequence").AsInt32().NotNullable()
                .WithColumn("Version").AsString().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("ModRelease");
            Delete.Table("ModTranslation");
            Delete.Table("Mod");
        }
    }
}
