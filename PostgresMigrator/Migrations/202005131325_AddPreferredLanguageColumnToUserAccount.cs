using FluentMigrator;

namespace Migrations
{
    [Migration(202005131325)]
    public class AddPreferredLanguageColumnToUserAccountAddPreferredLanguageColumnToUserAccount : Migration
    {
        public override void  Up()
        {
            Alter.Table("user_account")
                .AddColumn("preferred_language").AsGuid().ForeignKey("language", "id");
        }

        public override void Down()
        {
            Delete.Column("preferred_language").FromTable("user_account");
        }
    }
}