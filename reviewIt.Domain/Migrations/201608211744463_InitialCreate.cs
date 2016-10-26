namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessProfiles",
                c => new
                    {
                        BusinessId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(),
                        Location = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        Categories = c.String(),
                        ClosedorMoved = c.String(),
                        OpeningHours = c.String(),
                        Email = c.String(),
                        About = c.String(),
                    })
                .PrimaryKey(t => t.BusinessId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventDetails = c.String(),
                        Location = c.String(),
                        Schedule = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Events");
            DropTable("dbo.BusinessProfiles");
        }
    }
}
