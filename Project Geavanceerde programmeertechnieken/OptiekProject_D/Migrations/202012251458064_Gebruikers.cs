namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gebruikers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GebruikerTypes",
                c => new
                    {
                        GebruikerTypeID = c.Int(nullable: false, identity: true),
                        naam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GebruikerTypeID);
            
            CreateTable(
                "dbo.GPGebruikers",
                c => new
                    {
                        GebruikerID = c.Int(nullable: false, identity: true),
                        Mail = c.String(nullable: false),
                        Wachtwoord = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GebruikerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GPGebruikers");
            DropTable("dbo.GebruikerTypes");
        }
    }
}
