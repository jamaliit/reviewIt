namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wwww : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "About", c => c.String());
            AddColumn("dbo.UserProfiles", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "ImageName");
            DropColumn("dbo.UserProfiles", "About");
        }
    }
}
