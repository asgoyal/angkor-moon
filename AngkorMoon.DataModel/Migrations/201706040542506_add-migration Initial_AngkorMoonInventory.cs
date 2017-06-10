namespace AngkorMoon.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationInitial_AngkorMoonInventory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Long(nullable: false, identity: true),
                        ThirdPartyItemCode = c.String(),
                        CompanyName = c.String(),
                        ItemName = c.String(),
                        ItemType = c.String(),
                        MaterialType = c.String(),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.item_finding",
                c => new
                    {
                        ProductID = c.Long(nullable: false),
                        FindingID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.FindingID })
                .ForeignKey("dbo.Items", t => t.ProductID)
                .ForeignKey("dbo.Items", t => t.FindingID)
                .Index(t => t.ProductID)
                .Index(t => t.FindingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.item_finding", "FindingID", "dbo.Items");
            DropForeignKey("dbo.item_finding", "ProductID", "dbo.Items");
            DropIndex("dbo.item_finding", new[] { "FindingID" });
            DropIndex("dbo.item_finding", new[] { "ProductID" });
            DropTable("dbo.item_finding");
            DropTable("dbo.Items");
        }
    }
}
