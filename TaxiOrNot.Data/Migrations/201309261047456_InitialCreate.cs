namespace TaxiOrNot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Taxis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Information = c.String(),
                        DailyKmFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyBookingFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyInitialFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyMinFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NightlyKmFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NightlyBookingFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NightlyInitialFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NightlyMinFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaxiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Taxis", t => t.TaxiId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaxiId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PhoneId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaxiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Taxis", t => t.TaxiId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaxiId);
            
            CreateTable(
                "dbo.TaxiImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        UserId = c.Int(nullable: false),
                        TaxiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Taxis", t => t.TaxiId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaxiId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaxiImages", new[] { "TaxiId" });
            DropIndex("dbo.TaxiImages", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "TaxiId" });
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "TaxiId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Taxis", new[] { "CityId" });
            DropForeignKey("dbo.TaxiImages", "TaxiId", "dbo.Taxis");
            DropForeignKey("dbo.TaxiImages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Votes", "TaxiId", "dbo.Taxis");
            DropForeignKey("dbo.Votes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "TaxiId", "dbo.Taxis");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Taxis", "CityId", "dbo.Cities");
            DropTable("dbo.TaxiImages");
            DropTable("dbo.Votes");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Taxis");
            DropTable("dbo.Cities");
        }
    }
}
