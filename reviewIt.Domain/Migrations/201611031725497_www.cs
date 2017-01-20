namespace reviewIt.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class www : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        AdvertiseID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        About = c.String(),
                        Image = c.String(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdvertiseID);
            
            CreateTable(
                "dbo.Productcategories",
                c => new
                    {
                        ProductcategoryID = c.Int(nullable: false, identity: true),
                        BusinessCategoryId = c.Int(nullable: false),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ProductcategoryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Productcategories");
            DropTable("dbo.Advertisements");
        }
    }
}
