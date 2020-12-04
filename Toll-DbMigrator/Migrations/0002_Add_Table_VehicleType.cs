using FluentMigrator;

namespace Toll_DbMigrator.Migrations
{
    [Migration(0002)]
    class _0002_Add_Table_VehicleType : Migration
    {
        public override void Up()
        {
            Create.Table("VehicleType")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("IsFree").AsBoolean().NotNullable().WithDefaultValue(true);

            Insert.IntoTable("VehicleType").Row(new { Type = "Default", IsFree = false });
            Insert.IntoTable("VehicleType").Row(new { Type = "Military" });
            Insert.IntoTable("VehicleType").Row(new { Type = "Motorbike" });
            Insert.IntoTable("VehicleType").Row(new { Type = "Tractor" });
            Insert.IntoTable("VehicleType").Row(new { Type = "Emergency" });
            Insert.IntoTable("VehicleType").Row(new { Type = "Diplomat" });
            Insert.IntoTable("VehicleType").Row(new { Type = "Foreign" });
        }

        public override void Down()
        {
            Delete.Table("VehicleType");
        }
    }
}
