namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.BusinessProfiles", "CategoryId", c => c.String());
            DropTable("dbo.Events");
        }
        
        public override void Down()
        {
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
            
            DropColumn("dbo.BusinessProfiles", "CategoryId");
            DropTable("dbo.BusinessCategories");
        }
    }
}
