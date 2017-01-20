namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdfd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ProductName", c => c.String());
        }
    }
}
