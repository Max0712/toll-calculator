using FluentMigrator;

namespace Toll_DbMigrator.Migrations
{
    [Migration(0005)]
    public class _0005_Add_Table_VehicleTollEvent : Migration
    {
        public override void Up()
        {
            Create.Table("VehicleTollEvent")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("EventTime").AsDateTime().NotNullable()
                .WithColumn("RegistrationNumber").AsString().NotNullable()
                .WithColumn("VehicleId").AsInt64().Nullable();

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
