namespace Comics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginRegister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Hash", c => c.Binary());
            AddColumn("dbo.Users", "Salt", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Salt");
            DropColumn("dbo.Users", "Hash");
        }
    }
}
