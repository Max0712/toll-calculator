using FluentMigrator;

namespace Toll_DbMigrator.Migrations
{
    [Migration(0003)]
    public class _0003_Add_Table_Vehicle : Migration
    {
        public override void Up()
        {
            Create.Table("Vehicle")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("RegistrationNumber").AsString().NotNullable()
                .WithColumn("VehicleTypeId").AsGuid().NotNullable();

            Create.ForeignKey()
                .FromTable("Vehicle").ForeignColumn("VehicleTypeId")
                .ToTable("VehicleType").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey().FromTable("Vehicle").ForeignColumn("VehicleTypeId")
                .ToTable("VehicleType").PrimaryColumn("Id");

            Delete.Table("Vehicle");
        }
    }
}
