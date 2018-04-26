namespace Comics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURLAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "iamgeURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "iamgeURL");
        }
    }
}
