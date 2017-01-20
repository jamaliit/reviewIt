namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        BusinessId = c.Int(nullable: false),
                        ProductName = c.String(),
                        PostReview = c.String(),
                        Rating = c.String(),
                        Createdby = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
