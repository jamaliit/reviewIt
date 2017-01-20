
namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "CreatedDate");
        }
    }
}
