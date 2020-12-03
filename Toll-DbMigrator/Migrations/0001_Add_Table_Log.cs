using FluentMigrator;

namespace Toll_DbMigrator.Migrations
{
        [Migration(0001)]
        public class _0001_AddTable_Log : Migration
        {
            public override void Up()
            {
                Create.Table("Log")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Created").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                    .WithColumn("Message").AsString();
            }

            public override void Down()
            {
                Delete.Table("Log");
            }
        }

}
