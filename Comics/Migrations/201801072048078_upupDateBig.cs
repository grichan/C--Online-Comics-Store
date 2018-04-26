namespace Comics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upupDateBig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Orders", "product_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "product_id", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "total");
        }
    }
}
