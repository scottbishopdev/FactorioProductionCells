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

            Create.Table("language")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("english_name").AsString(50).NotNullable()
                .WithColumn("language_code").AsString(20).NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("user_account")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("email_address").AsString(200).NotNullable()
                .WithColumn("username").AsString(100).NotNullable().Unique()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("item")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
                .WithColumn("name").AsString(200).NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table("item_translation")
                .WithColumn("item_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("item", "id")
                .WithColumn("language_id").AsGuid().NotNullable().PrimaryKey().ForeignKey("language", "id")
                .WithColumn("display_name").AsString().NotNullable()
                .WithColumn("add_date").AsDateTimeOffset().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("item_translation");
            Delete.Table("item");
            
            Delete.Table("language");
            Delete.Table("user_account");

            IfDatabase("Postgres")
                .Execute.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";");
        }
    }
}