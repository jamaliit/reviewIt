namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ww : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "BusinessName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "BusinessName");
        }
    }
}
