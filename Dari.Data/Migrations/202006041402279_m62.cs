namespace Dari.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m62 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counselors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        cin = c.String(),
                        address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        image = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        userId = c.String(),
                        fb = c.String(),
                        tw = c.String(),
                        gg = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Counselors");
        }
    }
}
