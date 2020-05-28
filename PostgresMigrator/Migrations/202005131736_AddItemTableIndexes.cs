using FluentMigrator;

namespace Migrations
{
    [Migration(202005131736)]
    public class AddItemTableIndexes : Migration
    {
        public override void  Up()
        {
            Create.UniqueConstraint().OnTable("Item").Column("Name");
        }

        public override void Down()
        {
            Delete.UniqueConstraint().FromTable("Item").Column("Name");
        }
    }
}