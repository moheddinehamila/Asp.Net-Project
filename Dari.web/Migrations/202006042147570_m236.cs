namespace Dari.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m236 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false, maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String());
        }
    }
}
