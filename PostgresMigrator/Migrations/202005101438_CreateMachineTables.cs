using FluentMigrator;

namespace Migrations
{
    [Migration(202005101438)]
    public class CreateMachineTables : Migration
    {
        public override void  Up()
        {
            Create.Table("Machine")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("CraftingSpeed").AsDouble().NotNullable()
                .WithColumn("ModuleSlots").AsInt32().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("MachineTranslation")
                .WithColumn("MachineId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Machine", "Id")
                .WithColumn("LanguageId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Language", "Id")
                .WithColumn("DisplayName").AsString().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("MachineTranslation");
            Delete.Table("Machine");
        }
    }
}