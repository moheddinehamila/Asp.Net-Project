namespace Dari.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m585 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Verifications", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Verifications", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Verifications", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Verifications", "PhoneNumber", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Verifications", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Verifications", "Email", c => c.String());
            AlterColumn("dbo.Verifications", "LastName", c => c.String());
            AlterColumn("dbo.Verifications", "FirstName", c => c.String());
        }
    }
}
