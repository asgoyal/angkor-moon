namespace AngkorMoon.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "IsDirty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "IsDirty", c => c.Boolean(nullable: false));
        }
    }
}
