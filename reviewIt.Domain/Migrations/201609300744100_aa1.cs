namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessCategories", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessCategories", "CategoryName", c => c.Int(nullable: false));
        }
    }
}
