namespace Comics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "product_ids", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "product_ids");
        }
    }
}
