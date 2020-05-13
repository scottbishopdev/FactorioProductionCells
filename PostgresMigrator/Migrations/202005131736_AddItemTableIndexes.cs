using FluentMigrator;

namespace Migrations
{
    [Migration(202005131736)]
    public class AddItemTableIndexes : Migration
    {
        public override void  Up()
        {
            Create.UniqueConstraint().OnTable("item").Column("name");
        }

        public override void Down()
        {
            Delete.UniqueConstraint().FromTable("item").Column("name");
        }
    }
}