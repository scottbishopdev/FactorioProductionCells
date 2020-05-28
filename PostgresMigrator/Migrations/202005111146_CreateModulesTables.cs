using FluentMigrator;

namespace Migrations
{
    [Migration(202005111146)]
    public class CreateModulesTables : Migration
    {
        public override void  Up()
        {
            Create.Table("Module")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("ProductivityBonus").AsDouble().NotNullable()
                .WithColumn("SpeedBonus").AsDouble().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("ModuleTranslation")
                .WithColumn("ModuleId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Module", "Id")
                .WithColumn("LanguageId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Language", "Id")
                .WithColumn("DisplayName").AsString().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("ModuleTranslation");
            Delete.Table("Module");
        }
    }
}