using FluentMigrator;

namespace Toll_DbMigrator.Migrations
{
    [Migration(0005)]
    class _0005_Add_Table_VehicleTollEvent : Migration
    {
        public override void Up()
        {
            Create.Table("VehicleTollEvent")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("EventTime").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("VehicleId").AsGuid().NotNullable();

            Create.ForeignKey()
                .FromTable("VehicleTollEvent").ForeignColumn("VehicleId")
                .ToTable("Vehicle").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey().FromTable("VehicleTollEvent").ForeignColumn("VehicleId")
                       .ToTable("Vehicle").PrimaryColumn("Id");

            Delete.Table("VehicleTollEvent");
        }
    }
}
