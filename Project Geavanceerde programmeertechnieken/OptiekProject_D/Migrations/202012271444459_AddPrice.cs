namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modellen", "Prijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Sterktes", "Prijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sterktes", "Prijs");
            DropColumn("dbo.Modellen", "Prijs");
        }
    }
}
