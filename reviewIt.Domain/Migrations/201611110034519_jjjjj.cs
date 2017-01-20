namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jjjjj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "CreateDate");
        }
    }
}
