namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modellen", "Omschrijving", c => c.String());
            DropColumn("dbo.Brillen", "Omschrijving");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Brillen", "Omschrijving", c => c.String());
            DropColumn("dbo.Modellen", "Omschrijving");
        }
    }
}
