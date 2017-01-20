namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jjj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessProfiles", "ImageName", c => c.String());
            DropColumn("dbo.BusinessProfiles", "ClosedorMoved");
            DropColumn("dbo.BusinessProfiles", "OpeningHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BusinessProfiles", "OpeningHours", c => c.String());
            AddColumn("dbo.BusinessProfiles", "ClosedorMoved", c => c.String());
            DropColumn("dbo.BusinessProfiles", "ImageName");
        }
    }
}
