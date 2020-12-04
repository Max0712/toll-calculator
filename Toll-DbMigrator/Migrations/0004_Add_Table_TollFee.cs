using FluentMigrator;
using System;

namespace Toll_DbMigrator.Migrations
{
    [Migration(0004)]
    public class _0004_Add_Table_TollFee : Migration
    {
        public override void Up()
        {
            Create.Table("TollFee")
             .WithColumn("Id").AsInt64().PrimaryKey().Identity()
             .WithColumn("From").AsDateTime().NotNullable()
             .WithColumn("To").AsDateTime().NotNullable()
             .WithColumn("Fee").AsDecimal().NotNullable();

            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 06:00"), To = DateTime.Parse("1901-01-01 06:29"), Fee = 8 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 06:30"), To = DateTime.Parse("1901-01-01 06:29"), Fee = 13 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 07:00"), To = DateTime.Parse("1901-01-01 07:59"), Fee = 18 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 08:00"), To = DateTime.Parse("1901-01-01 08:29"), Fee = 13 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 08:30"), To = DateTime.Parse("1901-01-01 14:59"), Fee = 8 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 15:00"), To = DateTime.Parse("1901-01-01 15:29"), Fee = 13 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 15:30"), To = DateTime.Parse("1901-01-01 16:59"), Fee = 18 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 17:00"), To = DateTime.Parse("1901-01-01 17:59"), Fee = 13 });
            Insert.IntoTable("TollFee").Row(new { From = DateTime.Parse("1901-01-01 18:00"), To = DateTime.Parse("1901-01-01 18:29"), Fee = 8 });
        }

        public override void Down()
        {
            Delete.Table("TollFee");
        }
    }
}
