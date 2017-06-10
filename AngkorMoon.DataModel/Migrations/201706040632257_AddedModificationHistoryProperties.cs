namespace AngkorMoon.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModificationHistoryProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "IsDirty", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "IsDirty");
            DropColumn("dbo.Items", "DateCreated");
            DropColumn("dbo.Items", "DateModified");
        }
    }
}
