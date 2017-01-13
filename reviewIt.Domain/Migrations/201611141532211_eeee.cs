namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eeee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Advertisements", "About", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessProfiles", "BusinessName", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessProfiles", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessProfiles", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessProfiles", "About", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessProfiles", "About", c => c.String());
            AlterColumn("dbo.BusinessProfiles", "UserName", c => c.String());
            AlterColumn("dbo.BusinessProfiles", "Email", c => c.String());
            AlterColumn("dbo.BusinessProfiles", "BusinessName", c => c.String());
            AlterColumn("dbo.Advertisements", "About", c => c.String());
            AlterColumn("dbo.Advertisements", "Title", c => c.String());
        }
    }
}
