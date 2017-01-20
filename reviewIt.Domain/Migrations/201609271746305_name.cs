namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessProfiles", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BusinessProfiles", "UserName");
        }
    }
}
