namespace Dari.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnonceVentes",
                c => new
                    {
                        VenteId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                        Image = c.String(),
                        Adress = c.String(),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                        Category = c.String(),
                        Height = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Depth = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VenteId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Qte = c.Int(nullable: false),
                        FurnitureId = c.Int(),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BasketId)
                .ForeignKey("dbo.Furnitures", t => t.FurnitureId)
                .Index(t => t.FurnitureId);
            
            CreateTable(
                "dbo.Furnitures",
                c => new
                    {
                        FurnitureId = c.Int(nullable: false, identity: true),
                        Qte = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        description = c.String(nullable: false, maxLength: 225),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Depth = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.FurnitureId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Iso3 = c.String(nullable: false, maxLength: 3),
                        CountryNameEnglish = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Iso3);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        CouponId = c.Int(nullable: false, identity: true),
                        Couponvalue = c.String(),
                        Pourcentage = c.Int(nullable: false),
                        Used = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CouponId);
            
            CreateTable(
                "dbo.Estimates",
                c => new
                    {
                        EstimateId = c.Int(nullable: false, identity: true),
                        RegionCode = c.String(),
                        Type = c.Int(nullable: false),
                        MetrePrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstimateId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        BillId = c.Int(nullable: false),
                        Qte = c.Int(nullable: false),
                        FurnitureId = c.Int(),
                        TotalPrice = c.Double(nullable: false),
                        FinalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Furnitures", t => t.FurnitureId)
                .Index(t => t.FurnitureId);
            
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        RecommendationId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Budget = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        Adress = c.String(),
                        Price = c.Int(nullable: false),
                        DescriptionA = c.String(),
                        DescriptionR = c.String(),
                        UserId = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.RecommendationId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionCode = c.String(nullable: false, maxLength: 128),
                        Iso3 = c.String(nullable: false, maxLength: 3),
                        RegionNameEnglish = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RegionCode)
                .ForeignKey("dbo.Countries", t => t.Iso3, cascadeDelete: true)
                .Index(t => t.Iso3);
            
            CreateTable(
                "dbo.RendezVous",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idbuyer = c.String(),
                        idseller = c.String(),
                        title = c.String(),
                        color = c.String(),
                        start = c.String(),
                        end = c.String(),
                        allDay = c.Boolean(),
                        validappointment = c.Boolean(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Budget = c.Int(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 8),
                        TypeCh = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        Area = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Image = c.String(),
                        Title = c.String(),
                        UserId = c.String(maxLength: 100),
                        CountryIso3 = c.String(nullable: false, maxLength: 3),
                        RegionCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Countries", t => t.CountryIso3, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionCode)
                .Index(t => t.CountryIso3)
                .Index(t => t.RegionCode);
            
            CreateTable(
                "dbo.Verifications",
                c => new
                    {
                        verId = c.String(nullable: false, maxLength: 128),
                        cin = c.String(nullable: false, maxLength: 8),
                        address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        country = c.String(nullable: false),
                        image = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        userId = c.String(),
                        fb = c.String(),
                        tw = c.String(),
                        gg = c.String(),
                    })
                .PrimaryKey(t => t.verId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RegionCode", "dbo.Regions");
            DropForeignKey("dbo.Requests", "CountryIso3", "dbo.Countries");
            DropForeignKey("dbo.Regions", "Iso3", "dbo.Countries");
            DropForeignKey("dbo.Orders", "FurnitureId", "dbo.Furnitures");
            DropForeignKey("dbo.Baskets", "FurnitureId", "dbo.Furnitures");
            DropIndex("dbo.Requests", new[] { "RegionCode" });
            DropIndex("dbo.Requests", new[] { "CountryIso3" });
            DropIndex("dbo.Regions", new[] { "Iso3" });
            DropIndex("dbo.Orders", new[] { "FurnitureId" });
            DropIndex("dbo.Baskets", new[] { "FurnitureId" });
            DropTable("dbo.Verifications");
            DropTable("dbo.Requests");
            DropTable("dbo.RendezVous");
            DropTable("dbo.Regions");
            DropTable("dbo.Recommendations");
            DropTable("dbo.Orders");
            DropTable("dbo.Estimates");
            DropTable("dbo.Coupons");
            DropTable("dbo.Countries");
            DropTable("dbo.Furnitures");
            DropTable("dbo.Baskets");
            DropTable("dbo.AnnonceVentes");
        }
    }
}
