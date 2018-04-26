namespace Comics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "date");
        }
    }
}
