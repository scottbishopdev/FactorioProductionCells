using FluentMigrator;

namespace Migrations
{
    [Migration(202005131325)]
    public class AddPreferredLanguageColumnToUserAccountAddPreferredLanguageColumnToUserAccount : Migration
    {
        public override void  Up()
        {
            Alter.Table("UserAccount")
                .AddColumn("PreferredLanguage").AsGuid().ForeignKey("Language", "Id");
        }

        public override void Down()
        {
            Delete.Column("PreferredLanguage").FromTable("UserAccount");
        }
    }
}