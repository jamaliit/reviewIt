namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessProfiles", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessProfiles", "CategoryId", c => c.String());
        }
    }
}
