namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fgngkf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessProfiles", "isBusiness", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BusinessProfiles", "isBusiness");
        }
    }
}
