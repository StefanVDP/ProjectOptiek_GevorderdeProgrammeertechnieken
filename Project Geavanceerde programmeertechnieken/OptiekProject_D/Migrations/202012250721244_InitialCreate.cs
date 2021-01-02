namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brillen",
                c => new
                    {
                        BrilID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Omschrijving = c.String(),
                        Korting = c.Int(nullable: false),
                        Beschikbaar = c.Boolean(nullable: false),
                        ModelID = c.Int(nullable: false),
                        BriltypeID = c.Int(nullable: false),
                        SterkteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrilID)
                .ForeignKey("dbo.BrilTypes", t => t.BriltypeID, cascadeDelete: true)
                .ForeignKey("dbo.Modellen", t => t.ModelID, cascadeDelete: true)
                .ForeignKey("dbo.Sterktes", t => t.SterkteID, cascadeDelete: true)
                .Index(t => t.ModelID)
                .Index(t => t.BriltypeID)
                .Index(t => t.SterkteID);
            
            CreateTable(
                "dbo.BrilTypes",
                c => new
                    {
                        BriltypeID = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(),
                        Naam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BriltypeID);
            
            CreateTable(
                "dbo.Modellen",
                c => new
                    {
                        ModelID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        MerkID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModelID)
                .ForeignKey("dbo.Merken", t => t.MerkID, cascadeDelete: true)
                .Index(t => t.MerkID);
            
            CreateTable(
                "dbo.Kleurcombinaties",
                c => new
                    {
                        KleurCombinatieID = c.Int(nullable: false, identity: true),
                        KleurID = c.Int(nullable: false),
                        ModelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KleurCombinatieID)
                .ForeignKey("dbo.Kleuren", t => t.KleurID, cascadeDelete: true)
                .ForeignKey("dbo.Modellen", t => t.ModelID, cascadeDelete: true)
                .Index(t => t.KleurID)
                .Index(t => t.ModelID);
            
            CreateTable(
                "dbo.Kleuren",
                c => new
                    {
                        KleurID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KleurID);
            
            CreateTable(
                "dbo.Merken",
                c => new
                    {
                        MerkID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Oprichtingsdatum = c.DateTime(),
                        Omschrijving = c.String(),
                    })
                .PrimaryKey(t => t.MerkID);
            
            CreateTable(
                "dbo.Sterktes",
                c => new
                    {
                        SterkteID = c.Int(nullable: false, identity: true),
                        sterkte = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SterkteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brillen", "SterkteID", "dbo.Sterktes");
            DropForeignKey("dbo.Modellen", "MerkID", "dbo.Merken");
            DropForeignKey("dbo.Kleurcombinaties", "ModelID", "dbo.Modellen");
            DropForeignKey("dbo.Kleurcombinaties", "KleurID", "dbo.Kleuren");
            DropForeignKey("dbo.Brillen", "ModelID", "dbo.Modellen");
            DropForeignKey("dbo.Brillen", "BriltypeID", "dbo.BrilTypes");
            DropIndex("dbo.Kleurcombinaties", new[] { "ModelID" });
            DropIndex("dbo.Kleurcombinaties", new[] { "KleurID" });
            DropIndex("dbo.Modellen", new[] { "MerkID" });
            DropIndex("dbo.Brillen", new[] { "SterkteID" });
            DropIndex("dbo.Brillen", new[] { "BriltypeID" });
            DropIndex("dbo.Brillen", new[] { "ModelID" });
            DropTable("dbo.Sterktes");
            DropTable("dbo.Merken");
            DropTable("dbo.Kleuren");
            DropTable("dbo.Kleurcombinaties");
            DropTable("dbo.Modellen");
            DropTable("dbo.BrilTypes");
            DropTable("dbo.Brillen");
        }
    }
}
