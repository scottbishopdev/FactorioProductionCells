using FluentMigrator;

namespace Migrations
{
    [Migration(202005091230)]
    public class CreateInitialSchema : Migration
    {
        public override void  Up()
        {
            IfDatabase("Postgres")
                .Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

            Create.Table("Language")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("EnglishName").AsString(50).NotNullable()
                .WithColumn("LanguageCode").AsString(20).NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("UserAccount")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("EmailAddress").AsString(200).NotNullable()
                .WithColumn("Username").AsString(100).NotNullable().Unique()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("UpdateDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("Item")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("ItemTranslation")
                .WithColumn("ItemId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Item", "Id")
                .WithColumn("LanguageId").AsGuid().NotNullable().PrimaryKey().ForeignKey("Language", "Id")
                .WithColumn("DisplayName").AsString().NotNullable()
                .WithColumn("AddDate").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("ItemTranslation");
            Delete.Table("Item");
            
            Delete.Table("Language");
            Delete.Table("UserAccount");

            IfDatabase("Postgres")
                .Execute.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";");
        }
    }
}