namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixGebruikerTypeRelatie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GPGebruikers", "GebruikerTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.GPGebruikers", "GebruikerTypeID");
            AddForeignKey("dbo.GPGebruikers", "GebruikerTypeID", "dbo.GebruikerTypes", "GebruikerTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GPGebruikers", "GebruikerTypeID", "dbo.GebruikerTypes");
            DropIndex("dbo.GPGebruikers", new[] { "GebruikerTypeID" });
            DropColumn("dbo.GPGebruikers", "GebruikerTypeID");
        }
    }
}
