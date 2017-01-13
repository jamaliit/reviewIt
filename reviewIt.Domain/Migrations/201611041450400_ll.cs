namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "BusinessName", c => c.String());
            DropColumn("dbo.Advertisements", "BusinessId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "BusinessId", c => c.Int(nullable: false));
            DropColumn("dbo.Advertisements", "BusinessName");
        }
    }
}
