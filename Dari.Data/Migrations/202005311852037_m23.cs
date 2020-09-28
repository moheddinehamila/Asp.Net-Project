namespace Dari.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m23 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Verifications");
            AddColumn("dbo.Verifications", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Verifications", "Id");
            DropColumn("dbo.Verifications", "verId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Verifications", "verId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Verifications");
            DropColumn("dbo.Verifications", "Id");
            AddPrimaryKey("dbo.Verifications", "verId");
        }
    }
}
